<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="VisaRequest.aspx.cs" Inherits="TravelDesk.Employee.VisaRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
                                    function toggleValidatorBasedOnLevel() {
                                        var levelText = document.getElementById('<%= employeeLevel.ClientID %>').value;
                                            var level = parseInt(levelText);
                                            if (!isNaN(level)) {
                                                var validator = document.getElementById('<%= RequiredFieldValidator29.ClientID %>');
                                            if (level <= 9) {
                                                validator.enabled = true;
                                                validator1.enabled = true;

                                            } else {
                                                validator.enabled = false;
                                                validator1.enabled = false;

                                            }
                                        }
                                    }
                                    function showOthersFacility(dropdown) {
                                        var othersLabel = document.getElementById('<%= facOthersLbl.ClientID %>');
                                        var othersTextbox = document.getElementById('<%= othersFacility.ClientID %>');

                                        if (dropdown.value === "Others") {
                                            othersLabel.style.display = 'block';
                                            othersTextbox.style.display = 'block';
                                        } else {
                                            othersLabel.style.display = 'none';
                                            othersTextbox.style.display = 'none';
                                        }
                                    }

                                    function disableValidators() {
                                        document.getElementById('<%= RequiredFieldValidator29.ClientID%>').enabled = false;

                                        //EMPLOYEE INFORMATION
                                        document.getElementById('<%= RequiredFieldValidator2.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator5.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator20.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator3.ClientID %>').enabled = false;
					                    document.getElementById('<%= RequiredFieldValidator16.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator57.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator25.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator1.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator4.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator6.ClientID %>').enabled = false;

                                    }

    </script>
    <style>
                .required{
            color:red;
            font-size:14px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
               <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/visaRequests.png" style="width: 250px;" alt="logo.png">

                                </div>

                      </div>
                      <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                         <div class="page-body">
                                                <div class="card" style="color:black;">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Visa Request</h5>
                                                    </div>    
                                                            <div class="card-block">
                                                            <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                            <asp:DropDownList ID="homeFacility" runat="server" Style="margin-left: 80px" Width="345px"  onchange="showOthersFacility(this)">
                                                                <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True" />
                                                                <asp:ListItem Value="Legazpi">Legazpi</asp:ListItem>
                                                                <asp:ListItem Value="Mandaue">Mandaue</asp:ListItem>
                                                                <asp:ListItem Value="Manila">Manila</asp:ListItem>
                                                                <asp:ListItem Text="Others" Value="Others" />
                                                            </asp:DropDownList>   
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="homeFacility"></asp:RequiredFieldValidator>
                                                                <br />  <br />
                                                                <asp:Label ID="facOthersLbl" runat="server" Text="Others, please specify... " Style="display: none;"></asp:Label>
                                                                <asp:TextBox ID="othersFacility" runat="server" Width="320px" TextMode="MultiLine" Style="margin-left: 170px; display: none"></asp:TextBox>
                                                            </div>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:40px"  Width="300px"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeID"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="Label1" runat="server" Text="Project Code" Style="margin-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeeProjCode" runat="server" Width="300px" Style="margin-left: 40px"></asp:TextBox>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeProjCode"></asp:RequiredFieldValidator>
                                                              
                                                                <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:40px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 40px" Width="80px" onchange="toggleValidatorBasedOnLevel()"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeLevel"></asp:RequiredFieldValidator>
                                                               
                                                                <asp:Label ID="Label9" runat="server" Text="Department Unit"  style="padding-left:40px"></asp:Label>
                                                                <asp:TextBox ID="employeeDU" runat="server"   Style="margin-left: 40px" Width="200px" ></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeDU"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                                                                <asp:TextBox ID="employeeFName" runat="server" Width="300px" Style="margin-left: 50px"></asp:TextBox>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeFName"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label19" runat="server" Text="Middle Name" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeeMName" runat="server" Width="300px" Style="margin-left: 35px"></asp:TextBox>

                                                                <asp:Label ID="Label20" runat="server" Text="Last Name" Style="padding-left: 50px"></asp:Label>
                                                                <asp:TextBox ID="employeeLName" runat="server" Width="300px" Style="margin-left: 10px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeLName"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label3" runat="server" Text="Mobile Number"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeePhone"></asp:RequiredFieldValidator>
                                                               
                                                                <asp:Label ID="Label46" runat="server" Text="Email" Style="padding-left: 50px"></asp:Label>
                                                                <asp:TextBox ID="employeeEmail" runat="server" Width="300px" Style="margin-left: 75px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeEmail"></asp:RequiredFieldValidator>


                                                            </div>

                                                            <!--TRAVEL DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>    
                                                                <asp:TextBox ID="employeePurpose" runat="server" Width="300px" Style="margin-left: 20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeePurpose"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label14" runat="server" Text="Destination" Style="margin-left: 35px"></asp:Label>
                                                                <asp:TextBox ID="destination" runat="server" Width="320px" Style="margin-left: 35px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="destination"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label4" runat="server" Text="Estimated Travel Date" Style="margin-left: 50px"></asp:Label>
                                                                <asp:TextBox ID="estTravelDate" runat="server" TextMode="Date" Width="320px" Style="margin-left: 35px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="estTravelDate"></asp:RequiredFieldValidator>

                                                                <br />                                                         

                                                            </div>
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                            </div>
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                <div class="card-block" id="uploadBlock" runat="server">
                                                                                                    <asp:Label ID="Label15" runat="server" Text="Attach Email Approval"></asp:Label> <br />
                                                                                                   <asp:FileUpload ID="employeeUpload" type="file" runat="server" style="margin-left:100px"   Width="348px" />
                                                                                                    <asp:Button class="form-control btn btn-primary btn-sm"  ID="approvalUpload" runat="server" Text="Upload" Width="150px" CausesValidation="False" OnClick="approvalUpload_Click" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeUpload"></asp:RequiredFieldValidator>

                                                                                                </div>
                                                                                                <div class="col-md-5" style="display:none" id="pdfBlock" runat="server">
                                                                                                      <asp:Label ID="Label17" runat="server" Text="Manager Email Approval"></asp:Label>     <br />  <br />                                                           
                                                                                                            <asp:Button runat="server" class="btn btn-danger" Text="Delete" ID="reUpload" OnClick="reUpload_Click" CausesValidation="False" /> 
                                                                                                        <br /> <br />
                                                                                                            <iframe id="pdfViewer" runat="server" style="width:200%; height:600px;" frameborder="0"></iframe>
                                                           
                                                                                                 </div>

                                                                                            </div>
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" id="uploadPassport" runat="server">
                                                                                                    <asp:Label ID="Label24" runat="server" Text="Attach Scanned Copy of Passport"></asp:Label> <br />
                                                                                                   <asp:FileUpload ID="passportUpload" type="file" runat="server" style="margin-left:100px"  Width="348px" />
                                                                                                    <asp:Button class="form-control btn btn-primary btn-sm"  ID="uploadPassportbtn" runat="server" Text="Upload" Width="150px" CausesValidation="False" OnClick="uploadPassportbtn_Click" />
                                                                                               </div>
                                                                                                 <div class="col-md-5" style="display:none" id="passportBlock" runat="server">
                                                                                                  <asp:Label ID="Label25" runat="server" Text="Copy of Passport"></asp:Label>     <br />  <br />                                                           
                                                                                                        <asp:Button runat="server" class="btn btn-danger" Text="Delete" ID="passportReupload"  CausesValidation="False" OnClick="passportReupload_Click" /> 
                                                                                                    <br /> <br />
                                                                                                        <iframe id="passportViewer" runat="server" style="width:200%; height:600px;" frameborder="0"></iframe>
                                                           
                                                                                             </div>

                                                                                            </div>
                                                                                         </div>



                                           </div>
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit" ID="submitRequestbtn" OnClick="submitRequestbtn_Click"/>
<%--                                                             <asp:Button runat="server" class="btn btn-primary" Text="Save as Draft" ID="saveAsDraft" OnClientClick = "disableValidators();" OnClick="saveAsDraft_Click" />--%>

                                    <!-- Page-body end -->
                                </div>
                                <div id="styleSelector"> </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
         </div>

</asp:Content>
