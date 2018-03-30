<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="AuthorityChart.aspx.cs" Inherits="NDOnlineGym_2017.AuthorityChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />       
    <script src="JS/OfflineJavaScript.js"></script>
    
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        table, th, td {
    border: 1px solid black;
    border-collapse: collapse;
}
th, td {
    padding: 5px;
     
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="form-name-header">
            <h3>Authority Chart
                 <div class="navigation" >
                    <ul>
                        <li>Member Setting &nbsp; > &nbsp;</li>
                        <li>Status  &nbsp; > &nbsp;</li>
                        <li>Termination</li>
                    </ul>
                 </div>
            </h3>       
    </div>
  
        <div class="form-header">
            <h4>&#10148; Authority Chart  </h4>
        </div>
        
            <center style="margin-bottom:65px;">
                <div style="width:98%;border-bottom:1px solid black;border-right:1px solid black;">
                <table style="width:100%;" >
                    <tr>
                        <th rowspan="2">Right</th>
                        <th colspan="2">Edit/Update</th>
                         <th>Insert</th>
                         <th colspan="2">Delete</th>
                         <th colspan="2">Collection</th>
                         <th colspan="2">Branch</th>
                         <th colspan="2">Export To Excel</th>
                         
                    </tr>
                    <tr>
                        <th>Own</th>
                        <th>All</th>
                        <th>Own</th>
                       <%-- <th>All</th>--%>
                        <th>Own</th>
                        <th>All</th>
                        <th>Own</th>
                        <th>All</th>
                        <th>Own</th>
                        <th>All</th>
                        <th>Own</th>
                        <th>All</th>
                    </tr>
                    <tr>
                        <th><asp:Label ID="lblSuperAdmin" runat="server" Text="Super Admin"></asp:Label></th>
                        <td><asp:CheckBox ID="chkSAEditOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSAEditAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSAInsertOwn" runat="server" /></td>
                     <%--   <td><asp:CheckBox ID="chkSAInsertAll" runat="server" /></td>--%>
                        <td><asp:CheckBox ID="chkSADeleteOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSADeleteAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSACollectionOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSACollectionAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSABranchOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSABranchAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSAExportOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSAExportAll" runat="server" /></td>
                    </tr>
                     <tr>
                        <th><asp:Label ID="lblAdmin" runat="server" Text="Admin"></asp:Label></th>
                        <td><asp:CheckBox ID="chkAEditOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkAEditAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkAInsertOwn" runat="server" /></td>
                       <%-- <td><asp:CheckBox ID="chkAInsertAll" runat="server" /></td>--%>
                        <td><asp:CheckBox ID="chkADeleteOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkADeleteAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkACollectionOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkACollectionAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkABranchOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkABranchAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkAExportOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkAExportAll" runat="server" /></td>
                    </tr>
                    <tr>
                        <th><asp:Label ID="lblManager" runat="server" Text="Manager"></asp:Label></th>
                        <td><asp:CheckBox ID="chkMEditOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMEditAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMInsertOwn" runat="server" /></td>
                       <%-- <td><asp:CheckBox ID="chkMInsertAll" runat="server" /></td>--%>
                        <td><asp:CheckBox ID="chkMDeleteOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMDeleteAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMCollectionOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMCollectionAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMBranchOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMBranchAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMExportOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkMExportAll" runat="server" /></td>
                    </tr>
                    <tr>
                        <th><asp:Label ID="lblSuperUser" runat="server" Text="Super User"></asp:Label></th>
                        <td><asp:CheckBox ID="chkSUEditOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUEditAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUInsertOwn" runat="server" /></td>
                       <%-- <td><asp:CheckBox ID="chkSUInsertAll" runat="server" /></td>--%>
                        <td><asp:CheckBox ID="chkSUDeleteOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUDeleteAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUCollectionOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUCollectionAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUBranchOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUBranchAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUExportOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkSUExportAll" runat="server" /></td>
                    </tr>
                     <tr>
                        <th><asp:Label ID="lblUser" runat="server" Text="User"></asp:Label></th>
                        <td><asp:CheckBox ID="chkUEditOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUEditAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUInsertOwn" runat="server" /></td>
                       <%-- <td><asp:CheckBox ID="chkUInsertAll" runat="server" /></td>--%>
                        <td><asp:CheckBox ID="chkUDeleteOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUDeleteAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUCollectionOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUCollectionAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUBranchOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUBranchAll" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUExportOwn" runat="server" /></td>
                        <td><asp:CheckBox ID="chkUExportAll" runat="server" /></td>
                    </tr>
                </table>

                     
                </div>
                 <center class="btn-section">      
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" TabIndex="5" />

                        <asp:Button ID="btnClear1" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" />
                     </center>
            </center>
       
</asp:Content>
