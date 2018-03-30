<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receiptemail.aspx.cs" Inherits="NDOnlineGym_2017.receiptemail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <link href="CSS/Form.css" rel="stylesheet" />
     <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
   
    <link href="CSS/Receipt.css" rel="stylesheet" />
   
 <%-- <script>
      function OpenCourseReceipt() {

          window.open("Receipt.aspx", "_blank", "toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650");
      }
    </script>--%>

</head>
     <script type="text/javascript">
         $(function () {
             $("#btnPrint").click(function () {
                 var contents = $("#dvContainerPrint").html();
                 var frame1 = $('<iframe />');
                 frame1[0].name = "frame1";
                 frame1.css({ "position": "absolute", "top": "-1000000px" });
                 $("body").append(frame1);
                 var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                 frameDoc.document.open();
                 //Create a new HTML document.
                 frameDoc.document.write('<html><head><title>Bill & Receipts</title>');
                 frameDoc.document.write('</head><body>');
                 //Append the external CSS file.
                 frameDoc.document.write(' <link href="CSS/Receipt.css" rel="stylesheet"  type="text/css" />');
                 frameDoc.document.write("<html><head><style> #" + receipt + "{height:400px !important;font-size:12px;}</style>")
                 //Append the DIV contents.
                 frameDoc.document.write(contents);
                 frameDoc.document.write('</body></html>');
                 frameDoc.document.close();
                 setTimeout(function () {
                     window.frames["frame1"].focus();
                     window.frames["frame1"].print();
                     frame1.remove();
                 }, 500);
             });
         });
</script>
<body>
    <form id="form1" runat="server">
         <div  style="width:100%;">
        <div style="display: table;height: 100%;margin: 0 auto;">
            <div style="display: table-cell; vertical-align: middle;">
            
            <div>
                <div id="dvContainerPrint" style=" width:100%;border:1px solid black;padding:10px; ">
        <div id="dvContainer" runat="server" visible="true">
        
                <div style="width: 725px;" >
                    <div>


                <div style="display: table;height: 100%;margin: 0 auto;color:black" id="receipt">
                    <div style="display: table-cell;vertical-align: middle;color:black">
                   
                       

                           
                            <div style="float: left;margin:5px;text-align:center; ">
                              
                                <ul style=" width: 20%;padding: 0px 0px;text-align: left;float: left;list-style: none; margin: 0px; ">
                                    <li style="margin: 10px 0px 0px 0px;" >
                                        <%--<asp:Image ID="imgCompanyLogo" runat="server" AlternateText="Company Logo" ImageUrl="../Icons/DefaultLogo.png"
                                          style="width: 100px; height: 100px; border: solid 1px silver; border-radius: 4px; outline: none;" />--%>
                                    </li>
                                </ul>
                                <ul  style="width: 55%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin: 0px; margin: 10px 0px 0px 0px;" >
                                    <li style="text-align: center; width: 400px; font-size: x-large; font-weight: bold;  ">
                                        <asp:Label ID="lblCompanyName" runat="server" Text="My Dream Company"></asp:Label>
                                    </li>
                                    <li style="width: 55%;padding: 0px 0px;text-align: left; float: left;list-style: none; margin: 0px; text-align: center; width: 400px;" >
                                        <asp:Label ID="lblAddress1" runat="server" Text="Address1"></asp:Label>
                                    </li>
                                    <li style="width: 55%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin: 0px; text-align: center; width: 400px;" >
                                        <asp:Label ID="lblAddress2" runat="server" Text="Address2"></asp:Label>
                                    </li>
                                    <li style="width: 55%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin: 0px; text-align: center; width: 400px;" >
                                        <asp:Label ID="lblEmail" runat="server" ></asp:Label>
                                    </li>
                                </ul>
                                <ul style="width: 25%;padding: 0px 0px;text-align: left;float: right;list-style: none; margin: 0px;">
                                     <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;">
                                        <span><strong>Receipt Date :</strong></span>
                                        <asp:Label ID="lblReceiptDate" runat="server" Text="10/10/2017" ></asp:Label>
                                    </li>

                                     <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;">
                                        <span><strong>GST No. :</strong></span>
                                        <asp:Label ID="lblGSTNo" runat="server"></asp:Label>
                                    </li>

                                     <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;">
                                        <span id="Refer" runat="server"><strong>Refer Receipt No. :</strong></span>
                                        <asp:Label ID="lblReferReceiptNo" runat="server"></asp:Label>
                                    </li>
                                   
                                    <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;" id="ReceiptNo" runat="server">
                                        <span id="Span1" runat="server"><strong>Receipt No. :</strong></span>
                                        <asp:Label ID="lblReceiptNo" runat="server"></asp:Label>
                                    </li>
                                  
                                </ul></div><table><tr>
                                   <td style="margin-right:20px"> 
                                       <div style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;">
                                        <span><strong>Category :</strong></span>
                                        <asp:Label ID="lblCourseCategory" runat="server"  ></asp:Label>
                                    </div></td>
                                 <td  >
                                     <div style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 10px 0px 0px 0px;" id="group" runat="server" visible="false">
                                        <span><strong> Admin : </strong></span>
                                        <asp:Label ID="lblGroupOwner" runat="server" Text="Amol" ></asp:Label>
                                    </div></td></tr></table>
                          
                        <%--nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn--%>
                                     <table class="member-table"  style=""><tr><th>MID</th><th>Member Name</th><th>Contact</th><th>Email ID</th></tr>
                                           <asp:Repeater ID="RepterMemberDetails" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              <tr>
                                                 <td><span><%# Eval("Member_ID1")%></span></td>
                                                 <td><span><%# Eval("Name")%></span></td>
                                                 <td><span><%# Eval("Contact1")%></span></td>
                                                 <td><span><%# Eval("Email")%></span></td>
                                             </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                           </asp:Repeater>
                                     </table>   
                          
                                <table class="member-Course"  style="">
                                 <tr>
                                     <th>Package</th>
                                     <th>Session</th>
                                     <th>Duration</th>
                                     <th>Start Date</th>
                                     <th>End Date</th>
                                     <th>Discount</th>
                                     <th>Amount</th>
                                 </tr>
                                 <asp:Repeater ID="RepeaterCourseDetails" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                        <ItemTemplate>
                                          <tr>
                                             <td><span><%# Eval("Package")%></span></td>
                                             <td><span><%# Eval("Session")%></span></td>
                                             <td><span><%# Eval("Duration")%></span></td>
                                             <td><span><%# Eval("StartDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("EndDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("Discount")%></span></td>
                                             <td><span><%# Eval("Amount")%></span></td>
                                         </tr>
                                       </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                             </table>  
                         
                                <table class="member-Course"  style="">
                                 <tr>
                                     <th>Payment Mode</th>
                                     <th>Payment Date</th>
                                     <th>Amount</th>
                                     <th>GST %</th>
                                     <th>GST Amt</th>
                                     <th>Amt with GST</th>
                                 </tr>
                               <asp:Repeater ID="RepeaterPaymentDetails" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                        <ItemTemplate>
                                          <tr>
                                             <td><span><%# Eval("PaymentMode")%></span></td>
                                             <td><span><%# Eval("payDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("Paid")%></span></td>
                                             <td><span><%# Eval("taxpec")%></span></td>
                                              <td><span><%# Eval("TaxValue")%></span></td>
                                             <td><span><%# Eval("PaidWithTax")%></span></td>
                                         </tr>
                                         </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                                
                             </table>  
                      
                            <ul style="width: 20%;padding: 0px 0px;text-align: left;float: right;list-style: none; margin: 0px;">
                                    <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px;  margin: 10px 0px 0px 0px;">
                                        <span><strong>Total Fees :</strong></span>
                                        <asp:Label ID="lblReceiptTotal" runat="server"  ></asp:Label>
                                    </li>
                                     <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px;  margin: 10px 0px 0px 0px;">
                                        <span><strong>Paid Fees :</strong></span>
                                        <asp:Label ID="lblTotalPaid" runat="server"  ></asp:Label>
                                    </li>
                                    <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px;  margin: 10px 0px 0px 0px;">
                                        <span><strong>Total Discount :</strong></span>
                                        <asp:Label ID="lblTotalDiscount" runat="server"  ></asp:Label>
                                    </li>
                                     <li style=" width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px;  margin: 10px 0px 0px 0px;">
                                        <span><strong>Balance Fees :</strong></span>
                                        <asp:Label ID="lblBalance" runat="server" ></asp:Label>
                                    </li>
                                
                                   
                                </ul>
                            <div style="float: left; padding-bottom: 5px;width:730px" >
                                <ul style="width: 100%;padding: 0px 10px;list-style: none;float: left;">
                                    <li style=" width: 100%;padding: 0px 10px;list-style: none;float: left;">
                                        <span><strong>Terms And Condition's :</strong> </span>
                                    </li>
                                    <li  style=" width: 100%;padding: 0px 10px;list-style: none;float: left;">
                                        <asp:Label ID="lblNote1" runat="server" Text="Sincere"></asp:Label>
                                    </li>
                                    
                                    

                                  
                                </ul>
                                <ul style="width: 55%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin: 0px; text-align:left; margin-top: 35px;">
                                        <span><strong>Member Sign</strong></span>
                                    </ul>
                                 <ul style="width: 20%;padding: 0px 0px;text-align: left;float: right;list-style: none; margin: 0px;">
                                       <li style="width: 100%;padding: 0px 0px;text-align: left;float: left;list-style: none;margin-bottom: 10px; margin: 25px 0px 0px 0px;" >
                                        <span>Authorised Sign</span><br />
                                        <asp:Label ID="lblCompanyNamefooter" runat="server" Text="My Dream Company" Font-Bold="true"></asp:Label>
                                    </li>
                                   
                                </ul>
                            </div>
                        </div>
                    
                </div>

    </div></div></div></div>
            </div>
       </div>
       </div>
      
  </div>
    </form>
</body>
</html>
