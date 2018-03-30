<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="SMSTemplate.aspx.cs" Inherits="NDOnlineGym_2017.SMSTemplate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />

    <script src="JS/OfflineJavaScript.js"></script>

    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    
     <script type="text/javascript">
         function CountCharacters(txtMsg, indicator) {
             chars = txtMsg.value.length;
             document.getElementById(indicator).innerHTML = chars;
            
         }
    </script>
    <style>
         .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  >
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="sc">
        <div class="form-name-header">
        <h3>SMS Template
            <div class="navigation" >
            <ul>
                <li>File &nbsp; > &nbsp;</li>
                <li>SMS  &nbsp; > &nbsp;</li>
                <li>SMS Template</li>
            </ul>
            </div>
            <h3></h3>
        </h3>       
    </div>

    <div class="divForm" >

    <%--SMS Details--%>

        <div class="form-header">
            <h4>&#10148; SMS Templates</h4>
        </div>
        
        <div class="form-panel" >
            
            <table style="width:100%;">                                                 
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Member Birthday</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtMemberBirthdat" runat="server" CssClass="txt txt1" TabIndex="1"  TextMode="MultiLine" 
                                    onkeyup="CountCharacters(this,'lblcount1');" style="resize:none"></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount1" style="background-color:#E2EEF1;color:Red;font-weight:bold;" ></label>  
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                     <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Staff Birthday</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtStaffBirthday" runat="server" CssClass="txt txt1" TabIndex="2"  TextMode="MultiLine"
                                    onkeyup="CountCharacters(this,'lblcount2');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount2" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 

                            </td>
                         </tr></table>
                    </td>
                </tr>

                <tr>
                     <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Member Anniversary</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtMemberAnniversary" runat="server" CssClass="txt txt1" TabIndex="3"  TextMode="MultiLine"
                                    onkeyup="CountCharacters(this,'lblcount3');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count
                                    <label id="lblcount3" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Enquiry</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEnquiry" runat="server" CssClass="txt txt1" TabIndex="4"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount4');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount4" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Enquiry Followup</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEnquiryFollowup" runat="server" CssClass="txt txt1" TabIndex="5"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount5');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount5" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Registration</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtRegistration" runat="server" CssClass="txt txt1" TabIndex="6"  TextMode="MultiLine"
                                    onkeyup="CountCharacters(this,'lblcount6');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount6" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>

                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Upgrade</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtUpgrade" runat="server" CssClass="txt txt1" TabIndex="7"  TextMode="MultiLine" 
                                   onkeyup="CountCharacters(this,'lblcount7');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount7" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                    
                <tr>
                     <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Today's Balance</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtTodaysBalance" runat="server" CssClass="txt txt1" TabIndex="8"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount8');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount8" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>                        
                 </tr>

                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Balance Paid</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtBalance" runat="server" CssClass="txt txt1" TabIndex="9"  TextMode="MultiLine" 
                                   onkeyup="CountCharacters(this,'lblcount9');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount9" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>                        
                </tr>

                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%; text-align:left;"><span class="lbl">End Date Before 5 Days</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEndBefore" runat="server" CssClass="txt txt1" TabIndex="10"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount10');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount10" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">End Date Before 4 Days</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEndBefore4" runat="server" CssClass="txt txt1" TabIndex="11"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount11');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount11" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                 
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">End Date Before 3 Days</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEndBefore3" runat="server" CssClass="txt txt1" TabIndex="12"  TextMode="MultiLine" 
                                    onkeyup="CountCharacters(this,'lblcount12');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount12" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>

                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">End Date Before 2 Days</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEndBefore2" runat="server" CssClass="txt txt1" TabIndex="13"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount13');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount13" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">End Date Before 1 Days</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtEndBefore1" runat="server" CssClass="txt txt1" TabIndex="14"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount14');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount14" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>

                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Today's End Date</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtTodayEndDate" runat="server" CssClass="txt txt1" TabIndex="15"  TextMode="MultiLine"
                                   onkeyup="CountCharacters(this,'lblcount15');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount15" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
                <tr>
                    <td class="cols">
                        <table><tr>
                            <td style="width:25%;"><span class="lbl">Absent Member</span></td>
                            <td style="width:75%;text-align:left;">
                                <asp:TextBox ID="txtAbsentMember" runat="server" CssClass="txt txt1" TabIndex="16"  TextMode="MultiLine"
                                    onkeyup="CountCharacters(this,'lblcount16');" style="resize:none" ></asp:TextBox>
                                <br/>
                                 Count:
                                    <label id="lblcount16" style="background-color:#E2EEF1;color:Red;font-weight:bold;"></label> 
                            </td>
                         </tr></table>
                    </td>
                </tr>
                
            </table>

        </div>
                
        <center class="btn-section">
            <asp:Button ID="btnSave" runat="server" Text="Edit" CssClass="form-btn" TabIndex="17" OnClick="btnSave_Click"
                 OnClientClick=" this.disabled = true;" UseSubmitBehavior="false" />                                           
        </center>
    </div>


</div>
    </ContentTemplate>
         
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>--%>

    </asp:UpdatePanel>

</asp:Content>