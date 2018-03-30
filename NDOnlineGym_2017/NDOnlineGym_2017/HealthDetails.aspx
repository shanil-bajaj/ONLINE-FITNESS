<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="HealthDetails.aspx.cs" Inherits="NDOnlineGym_2017.HealthDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="CSS/Form.css" rel="stylesheet" />
    <link href="Toastr_CSS/toastr.min.css" rel="stylesheet" />
    <script src="Toastr_JS/jquery-1.11.0.min.js"></script>
    <script src="Toastr_JS/toastr.min.js"></script>
    <style>
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


     <script>

         function RestrictSpaceSpecial(e) {
             try {
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                     return false;
                 }
                 return true;
             }
             catch (err) {
                 alert(err.Description);
             }
         }

    </script>

    <script type = "text/javascript">
        function Radio_Click1() {
            var radio1 = document.getElementById("<%=rbtnDoctorCareYes.ClientID %>");
            var textBox = document.getElementById("<%=txtDoctorCareReason.ClientID %>");         
            textBox.disabled = !radio1.checked;
            if(!radio1.checked)
                textBox.value = "";
            textBox.focus();
        }

        function Radio_Click2() {
            var radio2 = document.getElementById("<%=rbtnMedicationYes.ClientID %>");
            var textBox = document.getElementById("<%=txtMedicationReason.ClientID %>");            
            textBox.disabled = !radio2.checked;
            if (!radio2.checked)
                textBox.value = "";
            textBox.focus();              
        }

        function Radio_Click3() {
            var radio3 = document.getElementById("<%=rbtnHospitalizedYes.ClientID %>");
            var textBox = document.getElementById("<%=txtHospitalizedReason.ClientID %>");            
            textBox.disabled = !radio3.checked;
            if (!radio3.checked)
                textBox.value=""
              textBox.focus();
        }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sc">
     <div class="form-name-header">
                <h3>Health Details
                 <div class="navigation">
                     <ul>
                         <li>File &nbsp; > &nbsp;</li>
                         <li>Health Details</li>
                     </ul>
                 </div>
                </h3>
            </div>

            <div class="divForm">

                <%--SMS Details--%>

                <div class="form-header">
                    <h4>&#10148; Health Details  </h4>
                </div>

                <div class="form-panel">
                    <fieldset class="field-section">
                        <legend>Member Details</legend>
                        <table class="MemberDetails" >
                            <tr>
                                <td class="section" >
                                    <strong>Member ID : </strong>
                                    <asp:TextBox ID="txtMemberID" runat="server" onkeypress="return RestrictSpaceSpecial(event);" OnTextChanged="txtMemberID_TextChanged" 
                                        AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                                <td class="section" >
                                    <strong>Member Name : </strong>
                                   <asp:TextBox ID="txtMemberName" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td class="section" >
                                    <strong>Contact No : </strong>
                                    <asp:TextBox ID="txtContact" runat="server" onkeypress="return RestrictSpaceSpecial(event);" OnTextChanged="txtContact_TextChanged" 
                                        AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                
                    <center>
                    <table class="DetailsSection">
                        <tr>
                            <td class="left-section" >- Are you currently Under a doctor's care : </td>
                            <td class="right-section" >
                                <asp:RadioButton ID="rbtnDoctorCareYes" runat="server" GroupName="a" onclick = "Radio_Click1()" /> Yes
                                <asp:RadioButton ID="rbtnDoctorCareNo" runat="server" GroupName="a" onclick = "Radio_Click1()" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section submenu" >If Yes, Explain : </td>
                            <td class="right-section" >
                                <asp:TextBox ID="txtDoctorCareReason" runat="server" CssClass="txt-box" Enabled = "false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- When was the last time you had a physical examination ? : </td>
                            <td class="right-section" >
                                <asp:TextBox ID="txtPhysicalExamination" runat="server" CssClass="txt-box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Have you ever had an exercise stress test : </td>
                            <td class="right-section" >
                                <asp:RadioButton ID="rbtnStressYes" runat="server" GroupName="b" /> Yes
                                <asp:RadioButton ID="rbtnStressNo" runat="server" GroupName="b" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section submenu" >If Yes, what is the result : </td>
                            <td class="right-section" >
                                <asp:RadioButton ID="rbtnStressResultYes" runat="server" GroupName="c" /> Normal
                                <asp:RadioButton ID="rbtnStressResultNo" runat="server" GroupName="c" />Abnormal
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Do you take any medications on regular basis ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnMedicationYes" runat="server" GroupName="d" onclick = "Radio_Click2()" /> Yes
                                <asp:RadioButton ID="rbtnMedicationNo" runat="server" GroupName="d" onclick = "Radio_Click2()"/>No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section submenu" >If Yes, Please list medications and reasons for taking : </td>
                             <td class="right-section" >
                                <asp:TextBox ID="txtMedicationReason" runat="server" CssClass="txt-box" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Have you been recently hospitalized ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnHospitalizedYes" runat="server" GroupName="e" onclick = "Radio_Click3()"/> Yes
                                <asp:RadioButton ID="rbtnHospitalizedNo" runat="server" GroupName="e" onclick = "Radio_Click3()" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section submenu" >If Yes, Explain : </td>
                            <td class="right-section" >
                                <asp:TextBox ID="txtHospitalizedReason" runat="server" CssClass="txt-box" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Do you smoke ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnSmokeYes" runat="server" GroupName="f" /> Yes
                                <asp:RadioButton ID="rbtnSmokeNo" runat="server" GroupName="f" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Are you pregnant ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnPregnantYes" runat="server" GroupName="i" /> Yes
                                <asp:RadioButton ID="rbtnPregnantNo" runat="server" GroupName="i" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Do you drink alcohol more than three times/week ? : </td>
                            <td class="right-section" >
                                  <asp:RadioButton ID="rbtnAlcoholYes" runat="server" GroupName="j" /> Yes
                                <asp:RadioButton ID="rbtnAlcoholNo" runat="server" GroupName="j" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Is your stress level high ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnStressLevelYes" runat="server" GroupName="k" /> Yes
                                <asp:RadioButton ID="rbtnStressLevelNo" runat="server" GroupName="k" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Are you moderately active on most days of week ? : </td>
                            <td class="right-section" >
                                  <asp:RadioButton ID="rbtnModeratelyYes" runat="server" GroupName="l" /> Yes
                                <asp:RadioButton ID="rbtnModeratelyNo" runat="server" GroupName="l" />No
                            </td>
                        </tr>
                        <tr>
                            <th class="left-section" >Do you have : </th>
                             <td class="right-section" ></td>
                        </tr>
                         <tr>
                            <td class="left-section" >- High Blood Pressure ? : </td>
                            <td class="right-section" >
                                <asp:RadioButton ID="rbtnBPStatusYes" runat="server" GroupName="m" /> Yes
                                <asp:RadioButton ID="rbtnBPStatusNo" runat="server" GroupName="m" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- High cholesterol ? : </td>
                            <td class="right-section" >
                                <asp:RadioButton ID="rbtnCholestrolYes" runat="server" GroupName="n" /> Yes
                                <asp:RadioButton ID="rbtnCholestrolNo" runat="server" GroupName="n" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Known heart disease ? : </td>
                            <td class="right-section" >
                                  <asp:RadioButton ID="rbtnHeartDiseaseYes" runat="server" GroupName="o" /> Yes
                                <asp:RadioButton ID="rbtnHeartDiseaseNo" runat="server" GroupName="o" />No
                            </td>
                        </tr>                         
                          <tr>
                            <td class="left-section" >- Rheumatic heart disease ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnRheumatiDiseaseYes" runat="server" GroupName="q" /> Yes
                                <asp:RadioButton ID="rbtnRheumatiDiseaseNo" runat="server" GroupName="q" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Chest Pain with exertion ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnChestPainYes" runat="server" GroupName="r" /> Yes
                                <asp:RadioButton ID="rbtnChestPainNo" runat="server" GroupName="r" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Irregular heart beat or palpitations ? : </td>
                            <td class="right-section" >
                                  <asp:RadioButton ID="btnHeartBeatYes" runat="server" GroupName="s" /> Yes
                                <asp:RadioButton ID="btnHeartBeatNo" runat="server" GroupName="s" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Lightheadness or do you  faint ? : </td>
                            <td class="right-section" >
                                  <asp:RadioButton ID="rbtnFaintYes" runat="server" GroupName="t" /> Yes
                                <asp:RadioButton ID="rbtnFaintNo" runat="server" GroupName="t" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Unusal shortness of breath ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnBreathYes" runat="server" GroupName="u" /> Yes
                                <asp:RadioButton ID="rbtnBreathNo" runat="server" GroupName="u" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Cramping pains in legs or feet ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnCrampingYes" runat="server" GroupName="v" /> Yes
                                <asp:RadioButton ID="rbtnCrampingNo" runat="server" GroupName="v" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Emphysema ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnEmphysemaYes" runat="server" GroupName="W" /> Yes
                                <asp:RadioButton ID="rbtnEmphysemaNo" runat="server" GroupName="w" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Other metabolic disorder(thyroid,kidney,etc) ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnMetabolicYes" runat="server" GroupName="x" /> Yes
                                <asp:RadioButton ID="rbtnMetabolicNo" runat="server" GroupName="x" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Epilepsy ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnEpilepsyYes" runat="server" GroupName="Y" /> Yes
                                <asp:RadioButton ID="rbtnEpilepsyNo" runat="server" GroupName="Y" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Asthma ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnAsthmaYes" runat="server" GroupName="Z" /> Yes
                                <asp:RadioButton ID="rbtnAsthmaNo" runat="server" GroupName="Z" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Back pain:upper,middle,lower ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnPainStatusYes" runat="server" GroupName="aa" /> Yes
                                <asp:RadioButton ID="rbtnPainStatusNo" runat="server" GroupName="aa" />No
                            </td>
                        </tr>
                         <tr>
                            <td class="left-section" >- Other joint pain ( Explian on back of forms) ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="rbtnOtherPainYes" runat="server" GroupName="bb" /> Yes
                                <asp:RadioButton ID="rbtnOtherPainNo" runat="server" GroupName="bb" />No
                            </td>
                        </tr>
                        <tr>
                            <td class="left-section" >- Muscle pain or an injury ( Explian on back of forms) ? : </td>
                            <td class="right-section" >
                                 <asp:RadioButton ID="btnMusclePainYes" runat="server" GroupName="cc" /> Yes
                                <asp:RadioButton ID="btnMusclePainNo" runat="server" GroupName="cc" />No
                            </td>
                        </tr>
                    </table>
                    </center>
                <center class="btn-section">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-btn" OnClick="btnSave_Click"  />
                    <asp:Button ID="btnCancle" runat="server" Text="Clear" CssClass="form-btn" OnClick="btnCancle_Click" />
                </center>
            </div>
        </div>
</asp:Content>
