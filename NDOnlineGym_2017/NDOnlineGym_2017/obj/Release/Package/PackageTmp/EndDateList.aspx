<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="EndDateList.aspx.cs" Inherits="NDOnlineGym_2017.EndDateList" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="sc">
     <div class="divForm">

        <div class="form-panel">
            <div class="form-name-header">
            <h3>End Date List
                 <div class="navigation" >
                    <ul>
                        <li>Member Setting &nbsp; > &nbsp;</li>
                        <li>Course &nbsp; > &nbsp;</li>
                        <li>End Date List</li>
                    </ul>
                 </div>
            </h3>
        </div>
            <div class="form-header">
                        <h4>&#10148;End Date List </h4>
                    </div>
            <table>
                    <tr>
                            <th>Form Date</th>
                            <th>To Date</th>
                            <th>Course</th>
                            <th>Duration</th>
                            <th>Session</th>
                            
                    </tr>
                    <tr>
                        <td>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1" style="width:200px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                                 <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2" style="width:200px"></asp:TextBox>
                                 <ajaxToolkit:CalendarExtender ID="txtToDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server" CssClass="ddl" TabIndex="3" style="width:200px" >
                                <asp:ListItem Value="--Select--">--Select Course--</asp:ListItem>
                               
                            </asp:DropDownList>

                        </td>

                        <td>
                             <asp:DropDownList ID="ddlDuration" runat="server" CssClass="ddl" TabIndex="4" style="width:200px">
                                <asp:ListItem Value="--Select--">--Select Duration--</asp:ListItem>
                               
                            </asp:DropDownList>
                        </td>
                         <td>
                             <asp:DropDownList ID="ddlSession" runat="server" CssClass="ddl" TabIndex="5" style="width:200px">
                                <asp:ListItem Value="--Select--">--Select Session--</asp:ListItem>
                               
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                
            </table>
            <center class="btn-section">
                <asp:Button ID="btnSearchbyEndDate" runat="server" CssClass="form-btn"  Text="Search by End Date" TabIndex="6" />
                 <asp:Button ID="btnSearchbyEndDateWithCategory" runat="server" CssClass="form-btn" TabIndex="7" Text="Search by End Date With Category" />
                 <asp:Button ID="btnSearchbyNextFollowup" runat="server" CssClass="form-btn" TabIndex="8" Text="Search by Next Followup" />
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="form-btn" TabIndex="9" Text="Export To Excel" />
            </center>

        </div>


                    <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 20px;">

                    <asp:GridView ID="gvEnquiry" runat="server" AutoGenerateColumns="false"  EmptyDataText="No record found." Width="1000px"
                        Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20" TabIndex="10" >

                        <Columns>
                            
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                        <RowStyle Height="20px" />
                        <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div>

                <div style="margin-top:10px;width:100%;">
                    <div style="margin-left:900px;">
                        <asp:Button ID="btnSendSMS" runat="server" CssClass="form-btn"  Text="SendSMS" TabIndex="11" />
                    </div>
                </div>

    </div>
        </div>
</asp:Content>
