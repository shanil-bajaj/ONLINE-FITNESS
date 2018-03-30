<%@ Page Title="" Language="C#" MasterPageFile="~/MasterGym.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NDOnlineGym_2017.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
      <link href="CSS/Form.css" rel="stylesheet" />
    <style>
        * { box-sizing: border-box;  }
        body {  height: 100%;margin: 0; padding: 0; color: #000000; font-family: Verdana, Geneva, sans-serif,'Times New Roman'; font-size: 11px;text-align: left;  }
        .divBranch {  padding: 10px; height: 320px;  }
        .divBranch th, .divBranch td {   border: 1px solid black; padding: 5px 10px; }
        .divBranch th { background-color: rgb(191, 191, 136); }
        .talk-bubble {	background-color: antiquewhite;padding: 5px 10px;width:400px;height:auto; display: inline-block;
                        position:relative;margin-top:10px;margin-left:20px;box-shadow:-2px 2px 10px 4px rgba(0, 0, 0, 0.20); }
        .talk-bubble1 {	padding: 5px;height:auto; display: inline-block;
                        position:relative;border-radius:3px;margin-bottom:10px }
        .tri-right.border.left-top:before {	content: ' ';position: absolute;width: 0;height: 0; left: -40px;right: auto; top: -8px;	
                                            bottom: auto;border: 32px solid;border-color: #666 transparent transparent transparent; }
        .tri-right.left-top:after{ content: ' ';position: absolute;	width: 0;height: 0; left: -15px;right: auto; top: 0px;bottom: auto;
	                               border: 16px solid;border-color: antiquewhite transparent transparent transparent; }

        .image-section-odd { width: 12% !important; padding: 5px;float:left; }
        .update-section-odd { width: 70% !important; padding: 5px;float:left; }
        .heading-section-odd { margin-left:10px;margin-top:-12px;font-size:15px;color:rgba(48, 46, 46, 0.99) }
        .description-section-odd { margin-left:10px;margin-top:-10px;width:800px;padding:3px; }
        .date-section-odd { text-align:end; width:800px; font-weight:bold; color:orangered; margin-top:5px; }

        .image-section-even { width: 12% !important; padding: 5px;float:right }
        .update-section-even { width: 70% !important; padding: 5px;float:left; }
        .heading-section-even { margin-top:-12px;text-align:end;right:0px;width:800px;margin-left:10px;font-size:15px;color:rgba(48, 46, 46, 0.99) }
        .description-section-even { margin-left:10px;margin-top:-10px;width:800px;padding:3px;text-align:end; }
        .date-section-even { width:800px; margin-left:15px; font-weight:bold; color:orangered ; margin-top:5px;}
 .gv{width: max-content;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Branch Section--%>
    <div class="divBranch">
        <h2 style="color: rgb(213, 86, 1);">Select Branch</h2>
        <%--<table style="width:100%;font-size:12px;border:1px solid silver;border-collapse:collapse;">
                <tr>
                    <th>Branch Name</th>
                    <th>Branch</th>
                    <th>City</th>
                    <th>Conatact</th>
                    <th>Address</th>
                </tr>
                <tr>
                    <td>Pune Branch</td>
                    <td>Aundh</td>
                    <td>Pune</td>
                    <td>9868678678</td>
                    <td>Aundh ,Pune</td>
                </tr>
                <tr>
                    <td>Pune Branch</td>
                    <td>Aundh</td>
                    <td>Pune</td>
                    <td>9868678678</td>
                    <td>Aundh ,Pune</td>
                </tr>
                <tr>
                    <td>Pune Branch</td>
                    <td>Aundh</td>
                    <td>Pune</td>
                    <td>9868678678</td>
                    <td>Aundh ,Pune</td>
                </tr>
                <tr>
                    <td>Pune Branch</td>
                    <td>Aundh</td>
                    <td>Pune</td>
                    <td>9868678678</td>
                    <td>Aundh ,Pune</td>
                </tr>
                <tr>
                    <td>Pune Branch</td>
                    <td>Aundh</td>
                    <td>Pune</td>
                    <td>9868678678</td>
                    <td>Aundh ,Pune</td>
                </tr>

            </table>--%>
        <div style="width: 1000px; height: 250px; overflow-x: scroll; overflow-y: scroll;">

            <asp:GridView ID="gvBranchInfo" runat="server" AutoGenerateColumns="false"  DataKeyNames="Branch_AutoID" EmptyDataText="No record found." 
                 AllowPaging="True" CssClass="gv"  OnPageIndexChanging="gvBranchInfo_PageIndexChanging" PageSize="20">

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" OnCommand="btnEdit_Command"
                                CommandArgument='<%#Eval("Branch_AutoID")%>' TabIndex="5"
                                 style="background-image:url('../NotificationIcons/edit.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                CommandArgument='<%#Eval("Branch_AutoID")%>' OnCommand="btnDelete_Command"
                                style="background-image:url('../NotificationIcons/f-cross_256-128.png');background-size:100% 100%;padding-left:13px;padding-top:0px;padding-bottom:3px;" ToolTip="Delete"  ></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Branch ID" DataField="Branch_ID1" HeaderStyle-HorizontalAlign="left" />
                    <asp:TemplateField HeaderText="Branch Name" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnName" runat="server" CausesValidation="false"
                                CommandArgument='<%#Eval("Branch_AutoID")%>' Text='<%#Eval("BranchName")%>' OnCommand="btnName_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact" ControlStyle-Width="200px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Contact1") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email ID" ControlStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Website" ControlStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtWebsite" runat="server" Text='<%#Eval("Website") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblWebsite" runat="server" Text='<%#Eval("Website") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" ControlStyle-Width="250px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtAddress1" runat="server" Text='<%#Eval("Address1") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAddress1" runat="server" Text='<%#Eval("Address1") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GST No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtGSTNo" runat="server" Text='<%#Eval("GSTNo") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblGSTNo" runat="server" Text='<%#Eval("GSTNo") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Contact1" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Website" DataField="Website" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Address1" DataField="Address1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="GSTNo" DataField="GSTNo" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />--%>
                </Columns>
            </asp:GridView>

        </div>
    </div>
    <%--End Branch Section--%>
	
	<%--Log Section--%>
    <div class="divBranch" id="divLogDet" runat="server">
        <h2 style="color: rgb(213, 86, 1);">Log Details</h2>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div style="margin-bottom:30px;padding:10px;">
        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
        </asp:DropDownList>
    </div>
        <div style="width: 1000px; height: 250px; overflow-x: scroll; overflow-y: scroll;">
           
            <asp:GridView ID="gvLogDetails" runat="server" AutoGenerateColumns="false"
                DataKeyNames="Log_AutoID" EmptyDataText="No record found." Width="1000px"
                 AllowPaging="True"
                OnPageIndexChanging="gvLogDetails_PageIndexChanging" PageSize="20">

                <Columns>
                    <%--<asp:TemplateField ControlStyle-Width="5px" HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" Text="Edit" OnCommand="btnEdit_Command"
                                CommandArgument='<%#Eval("Log_AutoID")%>' TabIndex="5" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ControlStyle-Width="5px" HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete?')" TabIndex="5"
                                CommandArgument='<%#Eval("Log_AutoID")%>' Text="Delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Branch Name" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtBranchName" runat="server" Text='<%#Eval("BranchName") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server" Text='<%#Eval("BranchName") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtDate" runat="server" Text='<%#Eval("Date","{0:dd-MM-yyyy}") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date","{0:dd-MM-yyyy}") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtTime" runat="server" Text='<%#Eval("Time") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Time") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Name") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Name") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile" ControlStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtContact1" runat="server" Text='<%#Eval("Mobile") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblContact1" runat="server" Text='<%#Eval("Mobile") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Email ID" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Username" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtWebsite" runat="server" Text='<%#Eval("Username") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblWebsite" runat="server" Text='<%#Eval("Username") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password" ControlStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtAddress1" runat="server" Text='<%#Eval("Password") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAddress1" runat="server" Text='<%#Eval("Password") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                    
                    <asp:TemplateField HeaderText="Status" ControlStyle-Width="80px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtStatus" runat="server" Text='<%#Eval("Status") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField HeaderText="Contact1" DataField="Contact1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Website" DataField="Website" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Address1" DataField="Address1" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="GSTNo" DataField="GSTNo" HeaderStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Status" DataField="Status" HeaderStyle-HorizontalAlign="left" />--%>
                </Columns>
            </asp:GridView>

        </div>
                </ContentTemplate></asp:UpdatePanel>
    </div>
    <%--End Log Section--%>

    <%--Notification and Update Section--%>
      

        <div style="width:100%;height:auto;padding-bottom:20px;padding-top:15px;background-color:whitesmoke; margin-top:20px">
           <center ><h1 style="color: rgb(213, 86, 1);">New Updates</h1></center>

                <%--New Update left Section--%>
                <div style="width: 100%;">
                  
                     
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="divodd"  style="width: 95%;margin-left:20px;" class="talk-bubble1"> 
                                    
                                        <div id="ImageSection" runat="server" value='<%# Eval("ImagePath") %>'>
                                            <asp:HiddenField Value='<%# Eval("ImagePath") %>' ID="HiddenField2" runat="server" />
                                            <asp:Image ID="ImgUpdate" runat="server" ImageUrl="../Icon/images (3).jpg" Width="100" Height="100"
                                                 Style="border-radius: 5px;box-shadow:-2px 2px 10px 4px rgba(0, 0, 0, 0.40);" />
                                        </div>
                                        <div id="UpdateSection" runat="server">
                                            
                                                <div id="divHeading" runat="server" >
                                                    <h3><asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading") %>'> </asp:Label></h3>
                                                </div>
                                                <div id="divDescription" runat="server" >
                                                        <div id="divUpdateInfo" >
                                                            <span id="spanNotiUpdate" runat="server" style="font-size: 12px;"><%# Eval("Information")%></span>
                                                        </div>
                                                </div>
                                              <div id="divdate" runat="server" >
                                                <span style="font-size:11px;">12 oct 2017</span>
                                              </div>
                                        </div>
                                  
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                   

                         <%--<div id="diveven" style="width: 95%;margin-left:20px;" class="talk-bubble1"> 
                                    
                                        <div style="" class="image-section-even">
                                         
                                            <asp:Image ID="ImgUpdate" runat="server" ImageUrl="../Icon/images (3).jpg" Width="100" Height="100" 
                                                Style="border-radius: 5px;box-shadow:-2px 2px 10px 4px rgba(0, 0, 0, 0.40);" />
                                        </div>
                                        <div style=""  class="update-section-even">
                                            
                                                <div class="heading-section-even" >
                                                    <h3><asp:Label ID="lblheading" runat="server" Text="Door Access"> </asp:Label></h3>
                                                </div>
                                                <div style="" class="description-section-even">
                                                        <div id="divUpdateInfo" >
                                                            <span id="spanNotiUpdate" runat="server" style="font-size: 12px;">
                                                               The Safe with inbuilt biometric fingerprint controller as well as two conventional locks (Lock A & Lock B) & Keys. To open the safe, 
                                                                first the two locks are to be opened with their keys, A & B. 
                                                                Thereafter two authorised users have to place their fingers on the reader one after the other. Only then does the door open.
                                                            </span>
                                                        </div>
                                                </div>
                                             <div class="date-section-even">
                                                <span style="color:rgba(48, 46, 46, 0.99);font-size:11px;">12 oct 2017</span>
                                              </div>
                                        </div>
                                </div>--%>
                  



                </div>
                <%--End New Update left Section--%>

                <%--New Update right Section--%>
                <%--<div style="width: 50%;float:left">
                   
                        <h2 style="color: rgb(213, 86, 1);">Staff Notification</h2>
                        <asp:Repeater ID="RepterDetails" runat="server" OnItemDataBound="RepterDetails_ItemDataBound">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="width: 100%;float:left;">
                                    
                                    <div style="width: 10% !important; padding: 5px;float:left">
                                        <div>
                                           
                                            <asp:HiddenField Value='<%# Eval("ImagePath") %>' ID="HiddenField1" runat="server" />
                                            <center><asp:Image ID="imgStaff" runat="server" ImageUrl="../Icon/user_male-black.png" Width="50" Height="50"
                                                Style="border-radius: 50px; background-color: silver;" /></center>
                                         </div>
                                        <div style="margin-left:5px;margin-top:-10px">
                                            <center><h4><asp:Label ID="Label2" runat="server" Text='<%# Eval("StaffName")%>'> </asp:Label></h4></center>
                                         </div> 
                                    </div>   
                                        <div style="width: 90% !important; padding: 5px;float:left">
                                            <div class="talk-bubble tri-right left-top">
                                                <span id="spanNoti" runat="server" style="font-size: 13px; " ><%# Eval("Notification")%></span>
                                                
                                            </div>
                                        </div>
                                  

                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                  
                </div>--%>
                <%--End New Update right Section--%>
           
        </div>
    
    <%--End Notification and Update Section--%>
</asp:Content>
