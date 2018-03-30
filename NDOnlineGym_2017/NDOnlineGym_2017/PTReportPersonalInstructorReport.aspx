<%@ Page Title="" Language="C#" MasterPageFile="~/PTMaster.Master" AutoEventWireup="true" CodeBehind="PTReportPersonalInstructorReport.aspx.cs" Inherits="NDOnlineGym_2017.PTReportPersonalInstructorReport" EnableEventValidation="false" %>

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

        .sc
        {
            width: 1021px;
        }

        @media screen and (min-width: 1400px)
        {
            .sc
            {
                width: 1100px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-name-header">
                    <h3>Instructor Report
           <div class="navigation">
               <ul>
                   <li>Report &nbsp; > &nbsp;</li>
                   <li>Instructor Report</li>
               </ul>
           </div>
                        <h3></h3>
                    </h3>
                </div>
                <div class="divForm">
                    <div class="form-panel">
                        <table style="width: 100%;">
                            <tr>
                                <td class="cols">
                                    <table style="margin-top: 25px">
                                        <tr>
                                            <td style="width: 45%;"><span class="lbl"><span class="error">*</span>Instructor Name</span></td>
                                            <td style="width: 55%; text-align: left;">
                                                <asp:DropDownList ID="ddlInstructor" runat="server" CssClass="ddl" TabIndex="1"  OnSelectedIndexChanged="ddlInstructor_SelectedIndexChanged">


                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStatus" runat="server" Display="Dynamic" CssClass="ErrorBox" ControlToValidate="ddlInstructor"
                                                    ErrorMessage="Select Status " SetFocusOnError="true" ValidationGroup="a" InitialValue="--Select--"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <center class="btn-section">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn"  TabIndex="2" OnClick="btnSearch_Click" />
                                     <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="3" OnClick="btnClear_Click" />
                                     <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="4" OnClick="btnExportToExcel_Click" />
                                 </center>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>

                <%-----------------------------------------------------------------------Report Section-----------------------------------------------------------------------%>

                <div class="divForm" style="margin-top: 5px">
                    <div class="form-panel">
                        <table style="margin-left: 900px;">
                            <tr>
                                <td>
                                    <span><strong>Total :</strong></span>
                                    <asp:Label ID="lblTotal" runat="server" Text="0" ForeColor="Green"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 1000px; overflow-x: auto; overflow-y: auto; margin-top: 20px;">
                            <asp:GridView ID="gvActiveDeactive" runat="server" AutoGenerateColumns="false" Width="990px"
                                Font-Size="13px" PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5"
                                AllowPaging="True" PageSize="20">
                                <Columns>
                                    <asp:BoundField HeaderText="Mem.ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Rec.ID" DataField="ReceiptID" HeaderStyle-HorizontalAlign="left" />
                                     <asp:BoundField HeaderText="Package Name" DataField="Package" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Duration" DataField="Duration" HeaderStyle-HorizontalAlign="left" />
                                   <asp:BoundField HeaderText="Session" DataField="Session" HeaderStyle-HorizontalAlign="left" />
                                     <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <b>Start.Date</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("StartDate","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField ItemStyle-Width="100px" ControlStyle-Width="100px">
                                        <HeaderTemplate>
                                            <b>End.Date</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("EndDate","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:BoundField HeaderText="TotalFees" DataField="TotalFee" HeaderStyle-HorizontalAlign="left" />

                                    <asp:BoundField HeaderText="Instructor" DataField="StaffName" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Days" DataField="Days" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Time" DataField="Time" HeaderStyle-HorizontalAlign="left" />  

                                     <asp:BoundField HeaderText="Incentive %" DataField="incentiveper" HeaderStyle-HorizontalAlign="left" />
                                                                
                                    <asp:BoundField HeaderText="Total Incentive" DataField="TotalIncentive" HeaderStyle-HorizontalAlign="left" />

                                    <asp:BoundField HeaderText="Compelete Session" DataField="SessionComplate" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Session Wise Incentive" DataField="SessionwiseIncentive" HeaderStyle-HorizontalAlign="left" />
                                    <asp:BoundField HeaderText="Rem. Session" DataField="RemSession" HeaderStyle-HorizontalAlign="left" />                              

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
                    <asp:PostBackTrigger ControlID="btnExportToExcel" />
                </Triggers>
        </asp:UpdatePanel> 
    </div>
</asp:Content>
