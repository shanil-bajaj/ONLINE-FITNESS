<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="NDOnlineGym_2017.Receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <link href="CSS/Form.css" rel="stylesheet" />
     <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
   
    <link href="CSS/Receipt.css" rel="stylesheet" />
   <style>
       .receipt-section { border:2px solid black;border-collapse:collapse; }
       .receipt-section td { padding:3px 6px;text-align: left;border:2px solid black;  }
       .row-heading { background-color:rgb(128, 128, 128);color:white; }
   </style>
 <%-- <script>
      function OpenCourseReceipt() {

          window.open("Receipt.aspx", "_blank", "toolbar=yes,scrollbars=no,resizable=yes,top=10,left=250,width=850,height=650");
      }
    </script>--%>
  <%--  <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ=" crossorigin="anonymous"></script>  
   <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.min.js"></script>  --%>
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
<%--<script>
    (function () {
        var
         form = $('.b'),
         cache_width = form.width(),
         a4 = [595.28, 841.89]; // for a4 size paper width and height  

        $('#create_pdf').on('click', function () {
            $('#dvContainerPrint').scrollTop(0);
            createPDF();
        });
        //create pdf  
        function createPDF() {
            getCanvas().then(function (canvas) {
                var
                 img = canvas.toDataURL("image/png"),
                 doc = new jsPDF({
                     unit: 'px',
                     format: 'a4'
                 });
                doc.addImage(img, 'JPEG', 20, 20);
                doc.save('Bhavdip-html-to-pdf.pdf');
                form.width(cache_width);
            });
        }

        // create canvas object  
        function getCanvas() {
            form.width((a4[0] * 1.33333) - 80).css('max-width', 'none');
            return html2canvas(form, {
                imageTimeout: 2000,
                removeContainer: true
            });
        }

    }());
</script>  
<script>
    /* 
 * jQuery helper plugin for examples and tests 
 */
    (function ($) {
        $.fn.html2canvas = function (options) {
            var date = new Date(),
            $message = null,
            timeoutTimer = false,
            timer = date.getTime();
            html2canvas.logging = options && options.logging;
            html2canvas.Preload(this[0], $.extend({
                complete: function (images) {
                    var queue = html2canvas.Parse(this[0], images, options),
                    $canvas = $(html2canvas.Renderer(queue, options)),
                    finishTime = new Date();

                    $canvas.css({ position: 'absolute', left: 0, top: 0 }).appendTo(document.body);
                    $canvas.siblings().toggle();

                    $(window).click(function () {
                        if (!$canvas.is(':visible')) {
                            $canvas.toggle().siblings().toggle();
                            throwMessage("Canvas Render visible");
                        } else {
                            $canvas.siblings().toggle();
                            $canvas.toggle();
                            throwMessage("Canvas Render hidden");
                        }
                    });
                    throwMessage('Screenshot created in ' + ((finishTime.getTime() - timer) / 1000) + " seconds<br />", 4000);
                }
            }, options));

            function throwMessage(msg, duration) {
                window.clearTimeout(timeoutTimer);
                timeoutTimer = window.setTimeout(function () {
                    $message.fadeOut(function () {
                        $message.remove();
                    });
                }, duration || 2000);
                if ($message)
                    $message.remove();
                $message = $('<div ></div>').html(msg).css({
                    margin: 0,
                    padding: 10,
                    background: "#000",
                    opacity: 0.7,
                    position: "fixed",
                    top: 10,
                    right: 10,
                    fontFamily: 'Tahoma',
                    color: '#fff',
                    fontSize: 12,
                    borderRadius: 12,
                    width: 'auto',
                    height: 'auto',
                    textAlign: 'center',
                    textDecoration: 'none'
                }).hide().fadeIn().appendTo('body');
            }
        };
    })(jQuery);

</script>  --%>


<body>
    <form id="form1" runat="server" class="form">
        <ul style="padding:0px;margin:0px;list-style:none;float:left">
                <li class="row-half-Twenty1" >
                  
                    <input type="button" value="Print" id="btnPrint" class="form-btn" />
                 <%--<input type="button" id="create_pdf" value="Generate PDF" />  --%>
                  <%-- <asp:Button ID="btnSendOnMail" runat="server" OnClick="btnSendOnMail_Click" />--%>
                </li>
            </ul>
        <asp:Panel ID="pnlPerson" runat="server">
         <div  style="width:100%;">
        <div class="grandParentContaniner1">
            <div class="parentContainer1">
            
            <div>
                <div id="dvContainerPrint" class="b"  style=" ">
        <div id="dvContainer" runat="server" visible="true">
        
                <div class="print-container a"  >
                    <div class="print-section">


                <div class="grandParentContaniner" id="receipt">
                    <div class="parentContainer">
                   
                       

                           
                            <div class="div1">
                              
                                <ul class="row-half-Twenty">
                                    <li class="row-half-Twenty n1" >
                                        <asp:Image ID="imgCompanyLogo" runat="server" AlternateText="Company Logo" ImageUrl="../Icons/DefaultLogo.png"
                                          CssClass="img-member" />
                                    </li>
                                </ul>
                                <ul class="row-half-Sixty n1" >
                                    <li class="row-half-Sixty CompanyName">
                                        <asp:Label ID="lblCompanyName" runat="server" Text="My Dream Company"></asp:Label>
                                    </li>
                                    <li class="row-half-Sixty n2" >
                                        <asp:Label ID="lblAddress1" runat="server" Text="Address1"></asp:Label>
                                    </li>
                                    <li class="row-half-Sixty n2" >
                                        <asp:Label ID="lblAddress2" runat="server" Text="Address2"></asp:Label>
                                    </li>
                                    <li class="row-half-Sixty n2" >
                                        <asp:Label ID="lblEmail" runat="server" ></asp:Label>
                                    </li>
                                </ul>
                               <%-- <ul class="row-half-Twenty1">
                                     <li class="row-half-Fourty n1">
                                        <span><strong>Receipt Date :</strong></span>
                                        <asp:Label ID="lblReceiptDate" runat="server" Text="10/10/2017" ></asp:Label>
                                    </li>

                                     <li class="row-half-Fourty n1">
                                        <span><strong>GST No. :</strong></span>
                                        <asp:Label ID="lblGSTNo" runat="server"></asp:Label>
                                    </li>

                                     <li class="row-half-Fourty n1">
                                        <span id="Refer" runat="server"><strong>Refer Receipt No. :</strong></span>
                                        <asp:Label ID="lblReferReceiptNo" runat="server"></asp:Label>
                                    </li>
                                   
                                    <li class="row-half-Fourty n1" id="ReceiptNo" runat="server">
                                        <span style="font-weight:bold;"><strong>Receipt No. :</strong></span>
                                        <asp:Label ID="lblReceiptNo" runat="server"></asp:Label>
                                    </li>
                                  
                                </ul>--%>
                                <table class="receipt-section" style="">
                                    <tr>
                                        <td><strong>Receipt Date :</strong></td>
                                        <td><asp:Label ID="lblReceiptDate" runat="server" Text="10/10/2017" ></asp:Label></td>
                                    </tr>
                                     <tr>
                                        <td><strong>GST No. :</strong></td>
                                        <td><asp:Label ID="lblGSTNo" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><span id="Refer" runat="server"><strong>Refer Receipt No. :</strong></span></td>
                                        <td><asp:Label ID="lblReferReceiptNo" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr id="ReceiptNo" runat="server"> 
                                        <td><strong>Receipt No. :</strong></td>
                                        <td><asp:Label ID="lblReceiptNo" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                            <table><tr>
                                   <td style="margin-right:20px"> <div class="row-half-Fourty n1">
                                        <span><strong>Category :</strong></span>
                                        <asp:Label ID="lblCourseCategory" runat="server"  ></asp:Label>
                                    </div></td>
                                 <td  ><div class="row-half-Fourty n1" id="group" runat="server" visible="false">
                                        <span><strong> Admin : </strong></span>
                                        <asp:Label ID="lblGroupOwner" runat="server" Text="Amol" ></asp:Label>
                                    </div></td>
                            </tr></table>
                          
                        
                                     <table class="member-table"  style="">
                                         <tr class="row-heading" style="">
                                             <th>MID</th>
                                             <th>Member Name</th>
                                             <th>Contact</th>
                                             <th>Gender</th>
                                             <th>Email ID</th>
                                         </tr>
                                           <asp:Repeater ID="RepterMemberDetails" runat="server">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                              <tr>
                                                 <td><span><%# Eval("Member_ID1")%></span></td>
                                                 <td><span><%# Eval("Name")%></span></td>
                                                 <td><span><%# Eval("Contact1")%></span></td>
                                                 <td><span><%# Eval("Gender")%></span></td>
                                                 <td><span><%# Eval("Email")%></span></td>
                                             </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                           </asp:Repeater>
                                     </table>   
                          
                                <table class="member-Course"  style="">
                                 <tr class="row-heading" >
                                     <th>Package</th>
                                     <th>Session</th>
                                     <th>Duration</th>
                                     <th>Start Date</th>
                                     <th>End Date</th>
                                     <th>Course Fees</th>
                                      <th>Instructor</th>
                                     <%-- <th>Discount</th>--%>
                                    
                                 </tr>
                                 <asp:Repeater ID="RepeaterCourseDetails" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                        <ItemTemplate>
                                          <tr>
                                             <td style="width:165px;"><span><%# Eval("Package")%></span></td>
                                             <td><span><%# Eval("Session")%></span></td>
                                             <td><span><%# Eval("Duration")%></span></td>
                                             <td><span><%# Eval("StartDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("EndDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("Amount")%></span></td>
                                              <td style="width:150px;"><span></span></td>                                          
                                              <td style="display:none;"><span><%# Eval("Discount")%></span></td>
                                             
                                         </tr>
                                       </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                             </table>  
                         
                                <table class="member-Course"  style="">
                                 <tr class="row-heading">
                                     <th>Pay Date</th>
                                     <th>Pay Mode</th>
                                     <th>Paid Fees</th>
                                     <th>Tax Type</th>
                                     <th>IGST (0%)</th>
                                     <th>CGST (<asp:Label ID="lblCGST" runat="server" Text="0" style="font-size:12px;"></asp:Label> % )</th>
                                     <th>SGST (<asp:Label ID="lblSGST" runat="server" Text="0" style="font-size:12px;" ></asp:Label> %) </th>
                                     <th>GST (<asp:Label ID="lblGST" runat="server" Text="0" style="font-size:12px;" ></asp:Label> %) </th>
                                     <th>Amt with GST</th>
                                 </tr>
                               <asp:Repeater ID="RepeaterPaymentDetails" runat="server">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                        <ItemTemplate>
                                          <tr>
                                             <td><span><%# Eval("payDate" , "{0:dd-MM-yyyy}")%></span></td>
                                             <td><span><%# Eval("PaymentMode")%></span></td>
                                             <td><span><%# Eval("Paid")%></span></td>
                                             <td><span><%# Eval("TaxType")%></span></td>
                                             <td><span>0</span></td>
                                             <td><span><%# Eval("val")%></span></td>
                                             <td><span><%# Eval("val")%></span></td>
                                             <td><span><%# Eval("TaxValue")%></span></td>
                                             <td><span><%# Eval("PaidWithTax")%></span></td>
                                         </tr>
                                         </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                                
                             </table>  
                            <div style="margin-top:10px" runat="server" visible="false">
                                <strong>Next Payment Date : </strong><asp:Label ID="lblNextPayDate" runat="server"></asp:Label>
                            </div>
                            <ul class="row-half-Twenty2">
                                    <li class="row-half-Fourty n1">
                                        <span><strong>Total Fees :</strong></span>
                                        <asp:Label ID="lblReceiptTotal" runat="server"  ></asp:Label>
                                    </li>
                                     <li class="row-half-Fourty n1">
                                        <span><strong>Paid Fees :</strong></span>
                                        <asp:Label ID="lblTotalPaid" runat="server"  ></asp:Label>
                                    </li>
                                    <li class="row-half-Fourty n1">
                                        <span><strong>Total Discount :</strong></span>
                                        <asp:Label ID="lblTotalDiscount" runat="server"  ></asp:Label>
                                    </li>
                                     <li class="row-half-Fourty n1">
                                        <span><strong>Balance Fees :</strong></span>
                                        <asp:Label ID="lblBalance" runat="server" ></asp:Label>
                                    </li>
                                
                                   
                                </ul>
                            <div class="div4" >
                                <ul class="row-full">
                                    <li class="row-full">
                                        <span><strong>Terms And Condition's :</strong> </span>
                                    </li>
                                    <li class="row-full">
                                        <asp:Label ID="lblNote1" runat="server" Text="Sincere"></asp:Label>
                                    </li>
                                    
                                    <li class="row-half-Sixty s1 membersign"  >
                                        <span><strong>Member Sign</strong></span>
                                    </li>

                                  
                                </ul>

                                 <ul class="row-half-Twenty2">
                                       <li class="row-half-Fourty n1" >
                                           <div class="div-sign" style="">
                                                <span>Authorised Sign</span><br />
                                                <asp:Label ID="lblCompanyNamefooter" runat="server" Text="My Dream Company" Font-Bold="true"></asp:Label>
                                           </div>
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
    </asp:Panel>
    </form>
</body>
</html>
