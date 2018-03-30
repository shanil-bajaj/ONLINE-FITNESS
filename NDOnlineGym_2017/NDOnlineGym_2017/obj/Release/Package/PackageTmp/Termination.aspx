<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Termination.aspx.cs" Inherits="NDOnlineGym_2017.Termination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Form.css" rel="stylesheet" />       
    <script src="JS/OfflineJavaScript.js"></script>
    
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>

     <style>
        .GridView { margin-top: 10px;float: left;border: solid 1px silver; border-radius: 3px; }
        .GridView a /** FOR THE PAGING ICONS  **/ { background-color: Transparent; padding: 5px 5px 5px 5px;color: black;text-decoration: none;font-weight: bold;  }
        .GridView a:focus { color: orangered; }
        .GridView a:hover { color: orangered; }
        .GridView span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {  /*color: #fff;*/ padding: 5px 5px 5px 5px;  }
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
            <h3>Termination
                 <div class="navigation" >
                    <ul>
                        <li>Member Setting &nbsp; > &nbsp;</li>
                        <li>Status  &nbsp; > &nbsp;</li>
                        <li>Termination</li>
                    </ul>
                 </div>
            </h3>       
    </div>
    
     <div class="divForm">
         
    <%--SMS Details--%>
         <div id="DivTermination" runat="server">
         <div class="form-header">
            <h4>&#10148; Termination  </h4>
        </div>
         <div class="form-panel">
            <table style="width:100%;">
                 <tr>
                     
                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Member ID </span></td>
                            <td style="width:55%;text-align:left;">
                                  <asp:TextBox ID="txtMemberID" runat="server" CssClass="txt" TabIndex="1" OnTextChanged="txtMemberID_TextChanged" AutoPostBack="true"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>  
                       <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Member Name </span></td>
                            <td style="width:55%;text-align:left;">
                                  <asp:TextBox ID="txtMemberName" runat="server" CssClass="txt" TabIndex="2" OnTextChanged="txtMemberName_TextChanged" AutoPostBack="true"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>    
                      <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Gender</span></td>
                            <td style="width:55%;text-align:left;">
                               <asp:DropDownList ID="ddlGender" runat="server" CssClass="ddl" TabIndex="3" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Male</asp:ListItem>
                                    <asp:ListItem Value="2">Female</asp:ListItem>
                               </asp:DropDownList>
                                
                            </td>
                         </tr></table>
                    </td>        
                                        
                </tr>       

                <tr>                  

                   <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl">Block/Unblock</span></td>
                            <td style="width:55%;text-align:left;">
                               <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" TabIndex="4" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                                     <asp:ListItem Value="1">All</asp:ListItem>
                                     <asp:ListItem Value="2">Block</asp:ListItem>
                                     <asp:ListItem Value="3">UnBlock</asp:ListItem>
                               </asp:DropDownList>
                               
                            </td>
                         </tr></table>
                    </td> 
                    <td class="cols"></td>
                   <td class="cols"></td>
                </tr>
              
                 <tr>
                     
                     <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl" style="font-weight:bold;font-size:14px;margin-left:85px;"> Block</span></td>
                            <td style="width:55%;text-align:left;">
                                  <asp:Label ID="lblBlock" runat="server"  TabIndex="4" Text="0" style="color:green;font-size:14px;font-weight:bold;margin-left:10px;"></asp:Label>
                               
                            </td>
                         </tr></table>
                    </td>  
                    
                      <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl" style="font-weight:bold;font-size:14px;margin-left:75px;"> Unblock </span></td>
                            <td style="width:55%;text-align:left;">
                                 <asp:Label ID="lblUnblock" runat="server"  TabIndex="4" Text="0" style="color:red;font-size:14px;font-weight:bold;margin-left:10px;"></asp:Label>
                               
                            </td>
                         </tr></table>
                    </td>    

                    <td class="cols">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl" style="font-weight:bold;font-size:14px;margin-left:99px;"> Total </span></td>
                            <td style="width:55%;text-align:left;">
                                 <asp:Label ID="lblTotal" runat="server"  TabIndex="4" Text="0" style="color:blue;font-size:14px;font-weight:bold;margin-left:10px;"></asp:Label>
                               
                            </td>
                         </tr></table>
                    </td>            
                                        
                </tr>       

                </table>
                <%--End SMS others Details--%>
              <center class="btn-section">      
                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="form-btn" TabIndex="5" />

                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnClick="btnClear_Click" />
             </center>
             </div>
              <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">

                       <%-- <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Block_AutoID" EmptyDataText="No record found." Width="2000px" PagerStyle-CssClass="pager"
                             CssClass="GridView" CellPadding="5" GridLines="None" 
                            PageSize="20" AllowPaging="True" TabIndex="27" >--%>
                      
                        <asp:GridView ID="gvTerminationDetails" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Branch_AutoID" EmptyDataText="No record found." Width="2000px" OnRowDataBound="gvTerminationDetails_RowDataBound"
                            CssClass="GridView"  CellPadding="5" PagerStyle-CssClass="pager" GridLines="None"
                            AllowPaging="True" OnPageIndexChanging="gvTerminationDetails_PageIndexChanging" PageSize="20" TabIndex="27" >

                             <Columns>                            

                           <asp:TemplateField ControlStyle-Width="5px" HeaderText="Block/Unblock" >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text='<%#Eval("BlockStatus")%>'  
                                CommandArgument='<%#Eval("Member_AutoID")%>'  TabIndex="32" OnCommand="btnEdit_Command" />                            
                        </ItemTemplate>
                          </asp:TemplateField>
                                 
                                 <asp:BoundField HeaderText="Member ID" DataField="Member_ID1" HeaderStyle-HorizontalAlign="left" >
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>

                            <asp:BoundField HeaderText="Member Name" DataField="MemberName" HeaderStyle-HorizontalAlign="left" >
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>
                           
                            <asp:BoundField HeaderText="Gender" DataField="Gender" HeaderStyle-HorizontalAlign="left" >
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>
                            <asp:BoundField HeaderText="Contact" DataField="Contact1" HeaderStyle-HorizontalAlign="left" >
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>
                              <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-HorizontalAlign="left" >
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                              </asp:BoundField>
                          <%--  <asp:BoundField HeaderText="Active/Deactive" DataField="BlockStatus" HeaderStyle-HorizontalAlign="left" >                         
                                 <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>--%>
                         
                        </Columns>
                           <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                          <RowStyle Height="20px" />
                          <AlternatingRowStyle Height="20px" BackColor="White" />

                 </asp:GridView>
              </div>
         </div>

         <div id="DivBlockUnblock">
          <div class="form-header" id="div1" runat="server" visible="false">
            <h4>&#10148; Block/Unblock Reason  </h4>
        </div>
         <div class="form-panel" id="div2" runat="server" visible="false">

             <asp:LinkButton ID="btnBack" runat="server" style="float:right;padding-right: 13px;" OnClick="btnBack_Click" CssClass="btnlink">Back</asp:LinkButton>
            <table>
                 <tr>
                      <td style="width:25%" >
                        <table><tr>
                            <td style="width:55%;"><span class="lbl" style="font-weight:bold;font-size:14px;"> Name : </span></td>
                            <td style="width:45%;text-align:left;">
                                 <asp:Label ID="lblName" runat="server"  TabIndex="4"  style="font-size:14px;font-weight:bold;margin-left:10px;"></asp:Label>
                               
                            </td>
                         </tr></table>
                    </td>    

                    <td style="width:25%">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Date </span></td>
                            <td style="width:55%;text-align:left;">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="txt" TabIndex="1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" Format="dd-MM-yyyy" />
                            </td>
                         </tr></table>
                    </td>  
                       <td style="width:25%">
                        <table><tr>
                            <td style="width:45%;"><span class="lbl"> Block/Unblock Reason </span></td>
                            <td style="width:55%;text-align:left;">
                                  <asp:TextBox ID="txtReason" runat="server" CssClass="txt" TabIndex="2" TextMode="MultiLine" style="resize:none;"></asp:TextBox>
                               
                            </td>
                         </tr></table>
                    </td>    
                     <td style="width:5%">
                     </td>      
                                        
                </tr>       

              

                </table>
                <%--End SMS others Details--%>
              <center class="btn-section">      
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" TabIndex="5" OnCommand="btnSave_Command" 
                  OnClientClick="this.disabled = true;" UseSubmitBehavior="false" />

                <asp:Button ID="btnClear1" runat="server" Text="Clear" CssClass="form-btn" TabIndex="6" OnCommand="btnClear1_Command"/>
             </center>
         </div>
    </div>
    

     <div style="width: 1000px; height: auto; overflow-x: scroll; margin-top: 50px;">
                            <%-- <asp:GridView ID="gvExpenseDetails" runat="server" AutoGenerateColumns="false" 
                             DataKeyNames="Exp_AutoID" EmptyDataText="No record found." Width="1000px" 
                            Font-Size="13px"  CssClass="GridView" PagerStyle-CssClass="pager"  GridLines="None"  AllowPaging="True"  pagesize="19"
                            OnPageIndexChanging="gvExpenseDetails_PageIndexChanging" >--%>

                          <asp:GridView ID="GVBlockUnblockStatus" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="Block_AutoID" EmptyDataText="No record found." Width="2000px" PagerStyle-CssClass="pager"
                             CssClass="GridView" CellPadding="5" GridLines="None" OnPageIndexChanging="GVBlockUnblockStatus_PageIndexChanging"
                            PageSize="20" AllowPaging="True" TabIndex="27" >

                           
                             <Columns>                            

                             <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-HorizontalAlign="left" >
                                     <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                             </asp:BoundField>
                           
                            <asp:BoundField HeaderText="Status" DataField="BlockStatus" HeaderStyle-HorizontalAlign="left" >
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Reason" DataField="BlockReason" HeaderStyle-HorizontalAlign="left" >
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:BoundField>
                          <%--  <asp:BoundField HeaderText="Active/Deactive" DataField="BlockStatus" HeaderStyle-HorizontalAlign="left" >                         
                                 <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                 </asp:BoundField>--%>
                         
                        </Columns>
                          <HeaderStyle Height="30px" BackColor="gray" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                          <RowStyle Height="20px" />
                          <AlternatingRowStyle Height="20px" BackColor="White" />

                 </asp:GridView>
              </div>
      
          </div>
        </div>
</ContentTemplate>
            </asp:UpdatePanel>
   
</asp:Content>
