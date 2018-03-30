<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewHealthDetails.aspx.cs" Inherits="NDOnlineGym_2017.ViewHealthDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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

       .MemberDetails { margin-top:10px;width:100%; }
       .section { width:33%; }
       .field-section { margin-bottom:10px }
       .DetailsSection { width:90%;margin-top:10px;border-collapse:collapse;padding:0px;margin:0px }
       .left-section { text-align:left;width:60%;padding:3px 5px;font-weight:bold }
       .right-section { text-align:right;width:40%;padding:3px 5px; }
       .submenu { padding-left:10px; }
       .txt-box { width:300px; }
        .sc { width:1021px;  }
         @media screen and (min-width: 1400px) {
             .sc { width:1100px; }
        }
    </style>

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

</head>
<body>
    <form id="form1" runat="server" class="form">
        <ul style="padding:0px;margin:0px;list-style:none;float:left">
                <li class="row-half-Twenty1" >                  
                    <input type="button" value="Print" id="btnPrint" class="form-btn" />                 
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
                                    </div>
                                    
                                   <%-- <table>
                                    <tr>
                                        <td style="margin-right:20px"> 
                                           <div class="row-half-Fourty n1">
                                                <span><strong>Category :</strong></span>
                                                <asp:Label ID="lblCourseCategory" runat="server"  ></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="row-half-Fourty n1" id="group" runat="server" visible="false">
                                                <span><strong> Admin : </strong></span>
                                                <asp:Label ID="lblGroupOwner" runat="server" Text="Amol" ></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>--%>
                            </div>                                  
                        </div>   
                        <div class="form-panel">
                            <fieldset class="field-section"> 
                                <legend>Member Details</legend>
                                    <table class="MemberDetails" style="">  
                                        <tr> 
                                            <td style="width:150px"><strong>Member Id :</strong></td>
                                            <td><asp:Label ID="lblMemberId" runat="server"></asp:Label></td>
                                        </tr>                                  
                                        <tr>
                                            <td style="width:150px"><strong>Member Name :</strong></td>
                                            <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width:150px"><strong>Contact :</strong></td>
                                            <td><asp:Label ID="lblContact" runat="server"></asp:Label></td>
                                        </tr>                                   
                                    </table>                   
                            </fieldset>
                        </div>
                    </div>
                            
                           
                    <table class="DetailsSection">
                        <tr>
                            <td class="left-section" >- Are you currently Under a doctor's care : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblDoctoreCare" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section submenu" >If Yes, Explain : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblDoctorCareReason" runat="server" ></asp:Label>                                    
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- When was the last time you had a physical examination ? : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblPhysicalExamination" runat="server" ></asp:Label>                                        
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Have you ever had an exercise stress test : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblStressStaus" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section submenu" >If Yes, were the result : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblStressResult" runat="server" ></asp:Label>                                   
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Do you take any medications on regular basis ? : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblMedication" runat="server" ></asp:Label>                                   
                            </td>
                        </tr>
                            <tr>
                            <td class="left-section submenu" >If Yes, Please list medications and reasons for taking : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblMedicationReason" runat="server" ></asp:Label>                                    
                            </td>
                        </tr>
                            <tr>
                            <td class="left-section" >- Have you been recently hospitalized ? : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblHospitalized" runat="server" ></asp:Label>                 
                            </td>
                        </tr>
                            <tr>
                            <td class="left-section submenu" >If Yes, Explain : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblHospitalizedReason" runat="server" ></asp:Label>                                      
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Do you smoke ? : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblSmoke" runat="server" ></asp:Label>                                     
                            </td>
                        </tr>
                            <tr>
                            <td class="left-section" >- Are you pregnant ? : </td>
                            <td class="left-section" >
                                <asp:Label ID="lblPregnant" runat="server" ></asp:Label>  
                            </td>
                        </tr>
                             <tr>
                                <td class="left-section" >- Do you drink alcohol more than three times/week ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblAlcohol" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Is your stress level high ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblStressLevel" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left-section" >- Are you moderately active on most days of week ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblModerately" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="left-section" >Do you have : </th>
                                <td class="left-section" ></td>
                            </tr>
                             <tr>
                                <td class="left-section" >- High Blood Pressure ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblBPStatus" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left-section" >- High cholesterol ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblCholestrol" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Known heart disease ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblHeartDisease" runat="server" ></asp:Label>                            
                                </td>
                            </tr>                         
                              <tr>
                                <td class="left-section" >- Rheumatic heart disease ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblRheumatiDisease" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Chest Pain with exertion ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblChestPain" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left-section" >- Irregular heart beat or palpitations ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblHeartBeat" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Lightheadness or do you  faint ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblFaint" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Unusal shortness of breath ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblBreath" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Cramping pains in legs or feet ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblCramping" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Emphysema ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblEmphysema" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Other metabolic disorder(thyroid,kidney,etc) ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblMetabolic" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Epilepsy ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblEpilepsy" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left-section" >- Asthma ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblAsthma" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Back pain:upper,middle,lower ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblPainStatus" runat="server" ></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="left-section" >- Other joint pain ( Explian on back of forms) ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblOtherPain" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left-section" >- Muscle pain or an injury ( Explian on back of forms) ? : </td>
                                <td class="left-section" >
                                    <asp:Label ID="lblMusclePain" runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>

                    </div>

                    </div></div>
            </div>
       </div>
       </div>
      </div>
    </asp:Panel>
    </form>
</body>
</html>
