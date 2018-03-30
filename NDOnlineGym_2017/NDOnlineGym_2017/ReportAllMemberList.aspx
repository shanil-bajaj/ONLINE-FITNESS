<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportAllMemberList.aspx.cs" Inherits="NDOnlineGym_2017.ReportAllMemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    
    <style>
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

        .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ { /*color: #fff;*/
        padding: 5px 5px 5px 5px;
        }
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
                    <h3>All Member List Report
                        <div class="navigation">
                            <ul>
                                <li>Report &nbsp; > &nbsp;</li>
                                <li>All Member List</li>
                            </ul>
                        </div>
                    </h3>
                </div>

                <div class="divForm">
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <center class="btn-section">
                            
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="form-btn" OnClick="btnShow_Click" TabIndex="1" />

                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="2" OnClick="btnClear_Click"/>

                                        <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CssClass="form-btn" TabIndex="3"  />
                        
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

        <%--    </ContentTemplate>
        </asp:UpdatePanel>--%>

    <%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    
   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>

            <div class="divForm" style="margin-top: 5px">
                <div class="form-panel">
                    <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                       <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                         <asp:GridView ID="gvReportAllMemberList" runat="server" AutoGenerateColumns="false" Width="1000px"
                            Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" 
                            OnPageIndexChanging="gvReportAllMemberList_PageIndexChanging" PageSize="20">

                            <Columns>
                                <asp:TemplateField HeaderText="Reg Date" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                    <ItemTemplate>
                                        <%# Eval("RegDate","{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" />
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                 <asp:BoundField HeaderText="Gender" DataField="Gender" />
                                 <asp:BoundField HeaderText="Contact No" DataField="Contact1" />
                                 <asp:BoundField HeaderText="Address" DataField="Address" />
                                <asp:TemplateField HeaderText="Course End Date" ItemStyle-Width="150px" ControlStyle-Width="150px">
                                    <ItemTemplate>
                                        <%# Eval("CourseEnd_Date","{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Status" DataField="Status" />
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
