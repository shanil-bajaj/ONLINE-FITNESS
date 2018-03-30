<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartMemberDetails.aspx.cs" Inherits="NDOnlineGym_2017.ChartMemberDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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

            .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
                /*color: #fff;*/
                padding: 5px 5px 5px 5px;
            }

        .txtTime {
            border: 1px solid silver;
            padding-left: 5px;
            float: left;
            width: 173px;
        }

        .btn-remove {
            background-color: rgb(248, 45, 70);
            color: white;
            border: 1px solid rgb(248, 45, 70);
            margin-top: 3px;
        }

            .btn-remove:focus {
                border: 1px solid black;
                cursor: pointer;
            }

        .btn-file:focus {
            border: 1px solid silver;
            cursor: pointer;
        }

        input[type="checkbox"]:focus {
            border-color: #ffffcc;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class="divForm">
      

            <table>
                       <tr>
                            <td>
                                <table>
                                    <tr>
                                        <th><span class="lbl">Package Name : </span></th>
                                        <td>
                                            <asp:Label ID="lblPackage" runat="server" Text="Label"></asp:Label>  
                                        </td>
                                    </tr>
                                </table>
                            </td>
                           <td></td>
                            <td>
                                <table>
                                    <tr>
                                        <th><span class="lbl">Duration : </span></th>
                                        <td>
                                            <asp:Label ID="lblDuration" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                </table>
             </div>
            <div style="width:850px;height:auto;overflow-x:scroll;margin-top:20px;">
              
                <asp:GridView ID="gvMember" runat="server" AutoGenerateColumns="false" 
                DataKeyNames="Member_AutoID" EmptyDataText="No record found." Width="1200" 
                      PagerStyle-CssClass="pager" CssClass="GridView" GridLines="None" CellPadding="5" AllowPaging="True"
                    PageSize="20" OnPageIndexChanging="gvMember_PageIndexChanging" >

                    <Columns>
                         <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnMember" runat="server" CausesValidation="false" 
                                                        CommandArgument='<%#Eval("Member_ID1")%>' Text='<%#Eval("Name")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                         <asp:BoundField DataField="Contact1" HeaderText="Contact" ItemStyle-Width="60px" >                                                </asp:BoundField>
                       
                         <asp:TemplateField HeaderText="Rigistration Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("RegDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                   
                             <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("StartDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                       
                        
                             <asp:TemplateField HeaderText="End Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("EndDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                       
                         <asp:BoundField DataField="Amount" HeaderText="Course Fees" ItemStyle-Width="60px" >                                                </asp:BoundField>
                         <asp:BoundField DataField="FinalTotal" HeaderText="Total Fees " ItemStyle-Width="60px" >                                                </asp:BoundField>
                       
                             <asp:TemplateField HeaderText="Pay Date" ItemStyle-Width="100px" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <%# Eval("payDate","{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                       <%--  --%>
                         <asp:BoundField DataField="StaffName" HeaderText="StaffName" ItemStyle-Width="60px" >                                                </asp:BoundField>
                    
                </Columns>
                       <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                       <RowStyle Height="20px" />
                       <AlternatingRowStyle Height="20px" BackColor="White" />
                </asp:GridView>
             </div>
    </form>
</body>
</html>
