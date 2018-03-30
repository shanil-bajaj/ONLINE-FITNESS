<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SendDataOnEmail.aspx.cs" Inherits="NDOnlineGym_2017.SendDataOnEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <style>
        table td
        {
            padding-bottom: 10px;
        }

        input:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ddl:focus
        {
            border: 1px solid rgb(242, 137, 9);
        }

        .ErrorBox
        {
            position: relative;
            z-index: 1;
            font-weight: normal;
            border-radius: 3px;
            box-shadow: 10px 10px 10px rgba(0, 0, 0, 0.5);
            padding: 5px 7px;
            color: #a94442;
            background-color: #f2dede;
            border: 1px solid #ebccd1;
        }

        .errorborder
        {
            border: 1px solid red;
        }
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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
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
                <h3>Send Data On Email
                 <div class="navigation">
                     <ul>
                         <li>File &nbsp; > &nbsp;</li>
                         <li>Company Setting  &nbsp; > &nbsp;</li>
                         <li>Send Data On Email</li>
                     </ul>
                 </div>
                 
                    <h3></h3>
                 
                    <h3></h3>
                 
                </h3>
            </div>

          
            <div class="divForm">
                <div class="form-header">
                    <h4>&#10148;Search By </h4>
                </div>
                <div class="form-panel">
                    <table style="width: 100%;">
                        <tr>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">From (Payment Date) </span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtFromDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server"
                                            BehaviorID="txtFromDate" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="cols">
                                <table>
                                    <tr>
                                        <td style="width: 45%;"><span class="lbl">To (Payment Date)</span></td>
                                        <td style="width: 55%; text-align: left;">
                                             <asp:TextBox ID="txtToDate" runat="server" CssClass="txt" TabIndex="2"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                            BehaviorID="txtToDate" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           <td class="cols">
                               <center>  
                                     <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="form-btn"  TabIndex="3" OnClick="btnSearch_Click"  />
                                     <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="form-btn" TabIndex="4" OnClick="btnSendMail_Click"   />
                                </center>
                           </td>

                        </tr>
                        
            </table>
                            
                </div>

                <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                    <asp:GridView ID="gvSendMail" runat="server" AutoGenerateColumns="False"  EmptyDataText="No record found." Width="1500px"
                      PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True" PageSize="20" TabIndex="15" OnPageIndexChanging="gvSendMail_PageIndexChanging" >
                        <Columns>
                              <asp:TemplateField HeaderText="Payment Date" ItemStyle-Width="200px" >
                         <ItemTemplate> 
                             <%# Eval("payDate","{0:dd-MM-yyyy}") %>
                         </ItemTemplate>
                     </asp:TemplateField>
                             <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" />                    
                            <asp:BoundField HeaderText="Receipt ID " DataField="ReceiptID" /> 
                             <asp:BoundField HeaderText="Member Name" DataField="MemberName" />
                            <asp:BoundField HeaderText="Contact " DataField="Contact1" /> 
                            <asp:BoundField HeaderText="Gender " DataField="Gender" /> 
                            <asp:BoundField HeaderText="Email " DataField="Email" /> 
                            <asp:BoundField HeaderText="Package " DataField="Package" /> 
                            <asp:BoundField HeaderText="Package Fee " DataField="Amount" /> 
                            <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="150px"  >
                         <ItemTemplate  > 
                             <%# Eval("StartDate","{0:dd-MM-yyyy}")  %>
                         </ItemTemplate>
                     </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="End Date" ItemStyle-Width="150px"  >
                         <ItemTemplate> 
                             <%# Eval("EndDate","{0:dd-MM-yyyy}") %>
                         </ItemTemplate>
                     </asp:TemplateField>
                            
                             <asp:BoundField HeaderText="Duration " DataField="Duration" /> 
                             <asp:BoundField HeaderText="Session " DataField="Session" /> 
                            <asp:BoundField HeaderText="Discount " DataField="Discount" /> 
                            <asp:BoundField HeaderText="No of People " DataField="Qty" /> 
                            <asp:BoundField HeaderText="Member Type " DataField="CourseMemberType" />                             
                            <asp:BoundField HeaderText="GST Type " DataField="TaxType" /> 
                            <asp:BoundField HeaderText="GST % " DataField="taxpec" /> 
                             <asp:BoundField HeaderText="GST Value " DataField="TaxValue" /> 
                             <asp:BoundField HeaderText="Paid Fee" DataField="TotalPaid" /> 
                             <asp:BoundField HeaderText="TotalFeeDue " DataField="TotalFeeDue" /> 
                             <asp:BoundField HeaderText="Balance " DataField="Balance" /> 

                     
                        </Columns>
                        <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <RowStyle Height="20px" />
                            <AlternatingRowStyle Height="20px" BackColor="White" />
                    </asp:GridView>

                </div>

            </div>
        </div>
        </ContentTemplate>
    <%--    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
