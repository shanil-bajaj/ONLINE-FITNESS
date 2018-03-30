<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ReportAllFollowup.aspx.cs" Inherits="NDOnlineGym_2017.ReportAllFollowup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
        .GridView
        {
            margin-top: 10px;
            float: left;
            border: solid 1px silver;
            border-radius: 3px;
        }

            .GridView a /** FOR THE PAGING ICONS  **/
            {
                background-color: Transparent;
                padding: 5px 5px 5px 5px;
                color: black;
                text-decoration: none;
                font-weight: bold;
            }

            .GridView a:focus
             {
                color: orangered;
             }

             .GridView a:hover
              {
                 color: orangered;
              }

           .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/
            { /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }
             .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>--%>
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <div class="sc">
    <div class="form-name-header">
        <h3>All Followup Report
           <div class="navigation">
               <ul>
                   <li>Report &nbsp; > &nbsp;</li>
                   <li>All Followup</li>
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
                                    <ajaxToolkit:CalendarExtender ID="txtFromDate_Calnderfromdate" runat="server" BehaviorID="txtRegDate_CalendarExtender" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />

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
                    <td class="cols"></td>
                </tr>
                <tr>
                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl">Search By Category</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <asp:DropDownList ID="ddlSearchByCategory" runat="server" CssClass="ddl"  TabIndex="3" >
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="SearchByExecutive">Search By Executive</asp:ListItem>
                                        <asp:ListItem Value="SearchByFollowupType">Search By FollowupType</asp:ListItem>
                                        <asp:ListItem Value="SearchByMemberID">Search By MemberID</asp:ListItem>
                                        <asp:ListItem Value="SearchByName">Search By Name</asp:ListItem>
                                        <asp:ListItem Value="SearchByContact">Search By Contact</asp:ListItem>
                                        <asp:ListItem Value="SearchByGender">Search By Gender</asp:ListItem>
                                        <asp:ListItem Value="SearchByRating">Search By Rating</asp:ListItem>
                                        <asp:ListItem Value="SearchByCallResponse">Search By CallResponse</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>


                    <td class="cols">
                        <table>
                            <tr>
                                <td style="width: 45%;"><span class="lbl">Search by</span></td>
                                <td style="width: 55%; text-align: left;">
                                    <%--<asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="ddl"  TabIndex="3">
                                        <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                      
                                    </asp:DropDownList>--%>   
                                     <asp:TextBox ID="txtSearch" runat="server" CssClass="txt" TabIndex="2" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>                          

                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cols"></td>
                </tr>
            </table>

            <div class="btn-section" style="margin-left: 80px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn" TabIndex="5" OnClick="btnSearch_Click"/>
                <asp:Button ID="btnSearchByFollowupDateCategory" runat="server" Text="FollowupDate With Category" CssClass="form-btn" TabIndex="5" OnClick="btnSearchByFollowupDateCategory_Click"/>
                <asp:Button ID="btnSearchByNextFollDateCategory" runat="server" Text="NextFollowupDate With Category" CssClass="form-btn" TabIndex="6" OnClick="btnSearchByNextFollDateCategory_Click" />
                <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn"  TabIndex="7" OnClick="btnCancle_Click" />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="8" />

            </div>
        </div>
    </div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    <%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>
    
    <div class="divForm" style="margin-top: 5px">
    <%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
        <div class="form-panel">
            <div style="width: 1000px; overflow-x: scroll; overflow-y: scroll; margin-top: 20px; border: 1px solid silver;">
                <div style="margin:10px 0px 10px 10px">
                            <asp:Label ID="lblCountHead" runat="server" Text="Total Records" Font-Bold="true" ForeColor="Black"></asp:Label>
                            <asp:Label ID="lblCount" runat="server" Text="0" Font-Bold="true" ForeColor="Black"></asp:Label>
                                </div>
                <asp:GridView ID="gvAllFollowupReport" runat="server" AutoGenerateColumns="false" Width="1000px"
                     Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                AllowPaging="True" PageSize="20">

                    <Columns>
                        <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-HorizontalAlign="left" />  
                        <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="CallResponse" DataField="CallResponse" HeaderStyle-HorizontalAlign="left" />             
                        <asp:BoundField HeaderText="Comment" DataField="Comment" HeaderStyle-HorizontalAlign="left" />             
                        <asp:BoundField HeaderText="Rating" DataField="Rating" HeaderStyle-HorizontalAlign="left" /> 
                        <asp:BoundField HeaderText="Followup Type" DataField="FollowupType" HeaderStyle-HorizontalAlign="left"/>
                        <asp:BoundField HeaderText="Followup Date" DataField="FollowupDate" HeaderStyle-HorizontalAlign="left" DataFormatString="dd-MM-yyyy" />
                        <asp:BoundField HeaderText="Next Followup Date" DataField="NextFollowupDate" HeaderStyle-HorizontalAlign="left" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField HeaderText="Executive" DataField="Executive" HeaderStyle-HorizontalAlign="left" />             
                     
                    </Columns>

                    <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                    <RowStyle Height="20px" />
                    <AlternatingRowStyle Height="20px" BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
          </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    
</asp:Content>
