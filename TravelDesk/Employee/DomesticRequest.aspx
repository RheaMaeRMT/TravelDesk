<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="DomesticRequest.aspx.cs" Inherits="TravelDesk.Employee.DomesticRequest" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
<!--javascript for the dropdown: manager approval options-->
                                <script type="text/javascript">
                                    function showHideOthers(dropdown) {
                                        var othersLabel = document.getElementById('<%= Label14.ClientID %>');
                                                                    var othersTextbox = document.getElementById('<%= otherspecified.ClientID %>');

                                                                    if (dropdown.value === "Others") {
                                                                        othersLabel.style.display = 'block';
                                                                        othersTextbox.style.display = 'block';
                                                                    } else {
                                                                        othersLabel.style.display = 'none';
                                                                        othersTextbox.style.display = 'none';
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

                                    function flightSelection() {
                                        var selectedOption = document.getElementById('<%= flightOptions.ClientID %>').value;

                                        // Hide all blocks initially
                                        document.getElementById('<%= oneWaynput.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= roundTripInput.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= multipleInput.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= destination3.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= destination4.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= destination5.ClientID %>').style.display = 'none';

                                        // Disable all validators initially
                                        disableAllValidators();

                                        // Determine which block to display based on the selected option
                                        if (selectedOption === "One Way") {
                                            disableMultiple();
                                            disableRound();
                                            document.getElementById('<%= oneWaynput.ClientID %>').style.display = 'block';
                                            document.getElementById('<%= destination3.ClientID %>').style.display = 'none';
                                            document.getElementById('<%= destination4.ClientID %>').style.display = 'none';
                                            document.getElementById('<%= destination5.ClientID %>').style.display = 'none';

                                            enableValidators('<%= RequiredFieldValidator13.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator4.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator6.ClientID %>');

                                            // DISABLE the validators associated with the multiple block
                                            disableMultiple();

                                            // Enable the validators associated with the roundtrip block
                                            disableRound();
                                            



                                        } else if (selectedOption === "Roundtrip") {

                                            disableOneway();
                                            disableMultiple();

                                            document.getElementById('<%= roundTripInput.ClientID %>').style.display = 'block';
                                            document.getElementById('<%= destination3.ClientID %>').style.display = 'none';
                                            document.getElementById('<%= destination4.ClientID %>').style.display = 'none';
                                            document.getElementById('<%= destination5.ClientID %>').style.display = 'none';

                                            // disable the validators associated for the one-way block
                                            disableOneway();

                                            // DISABLE the validators associated with the multiple block
                                            disableMultiple();

                                            enableValidators('<%= RequiredFieldValidator17.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator18.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator8.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator7.ClientID %>');

                                        } else if (selectedOption === "multiple") {
                                            document.getElementById('<%= multipleInput.ClientID %>').style.display = 'block';
                                            enableValidators('<%= RequiredFieldValidator21.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator22.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator26.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator23.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator24.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator28.ClientID %>');

                                            enableValidators('<%= RequiredFieldValidator50.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator32.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator9.ClientID %>');

                                            enableValidators('<%= RequiredFieldValidator10.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator11.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator12.ClientID %>');

                                            enableValidators('<%= RequiredFieldValidator14.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator15.ClientID %>');
                                            enableValidators('<%= RequiredFieldValidator19.ClientID %>');

                                            document.getElementById('<%= add3rd.ClientID %>').style.display = 'block';

                                            
                                            // disable the validators associated for the one-way block
                                            disableOneway();
                                            // disable the validators associated with the roundtrip block
                                            disableRound();


                                        }
                                    }

                                    function enableValidators(validatorId) {
                                        document.getElementById(validatorId).disabled = false;
                                    }

                                    function disableAllValidators() {
                                        var validators = document.querySelectorAll('.validator');
                                        validators.forEach(function (validator) {
                                            validator.disabled = true; // Use disabled property to disable the validator
                                        });
                                    }


                                    function add3rd(button) {
                                        document.getElementById('<%= destination3.ClientID %>').style.display = 'block';
                                        document.getElementById('<%= add4th.ClientID %>').style.display = 'block';
                                        document.getElementById('<%= add3rd.ClientID %>').style.display = 'none';

                                        //ENABLE 3RD DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator50.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator32.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator9.ClientID %>').enabled = true;

                                        //DISABLE 4TH DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator10.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator11.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator12.ClientID %>').enabled = false;

                                        //DISABLE 5TH DESTINATION VALIDATORS
                                            document.getElementById('<%= RequiredFieldValidator14.ClientID %>').enabled = false;
                                            document.getElementById('<%= RequiredFieldValidator15.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator19.ClientID %>').enabled = false;


                                    }
                                    function add4th(button) {
                                        document.getElementById('<%= destination4.ClientID %>').style.display = 'block';
                                        document.getElementById('<%= add4th.ClientID %>').style.display = 'none';
                                        document.getElementById('<%= add5th.ClientID %>').style.display = 'block';

                                        //ENABLE 4TH DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator10.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator11.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator12.ClientID %>').enabled = true;

                                        //DISABLE 3RD DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator50.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator32.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator9.ClientID %>').enabled = false;
                                        //DISABLE 5TH DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator14.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator15.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator19.ClientID %>').enabled = false;

                                    }
                                    function add5th(button) {

                                        document.getElementById('<%= destination5.ClientID %>').style.display = 'block';
                                        document.getElementById('<%= add5th.ClientID %>').style.display = 'none';

                                        //DISABLE 5TH DESTINATION VALIDATORS
                                        document.getElementById('<%= RequiredFieldValidator14.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator15.ClientID %>').enabled = true;
                                        document.getElementById('<%= RequiredFieldValidator19.ClientID %>').enabled = true;
                                    }
                                    
                                    function toggleValidatorBasedOnLevel() {
                                        var levelText = document.getElementById('<%= employeeLevel.ClientID %>').value;
                                            var level = parseInt(levelText);
                                            if (!isNaN(level)) {
                                            var validator = document.getElementById('<%= RequiredFieldValidator29.ClientID %>');
                                            if (level <= 9) {
                                                validator.enabled = true;
                                            } else {
                                                validator.enabled = false;
                                            }
                                        }
                                    }
                                    function disableOneway() {
                                        // disable the validators associated for the one-way block
                                        document.getElementById('<%= RequiredFieldValidator13.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator4.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator6.ClientID %>').enabled = false;
                                    }
                                    function disableRound() {
                                        // disable the validators associated with the roundtrip block
                                        document.getElementById('<%= RequiredFieldValidator17.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator18.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator8.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator7.ClientID %>').enabled = false;
                                    }
                                   function disableMultiple(){
                                        // DISABLE the validators associated with the multiple block
                                        document.getElementById('<%= RequiredFieldValidator21.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator22.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator26.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator23.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator24.ClientID %>').enabled = false;
                                       document.getElementById('<%= RequiredFieldValidator28.ClientID %>').enabled = false;

                                       document.getElementById('<%= RequiredFieldValidator50.ClientID %>').enabled = false;
                                       document.getElementById('<%= RequiredFieldValidator32.ClientID %>').enabled = false;
                                       document.getElementById('<%= RequiredFieldValidator9.ClientID %>').enabled = false;


                                       document.getElementById('<%= RequiredFieldValidator10.ClientID %>').enabled = false;
                                       document.getElementById('<%= RequiredFieldValidator11.ClientID %>').enabled = false;
                                            document.getElementById('<%= RequiredFieldValidator12.ClientID %>').enabled = false;

                                            document.getElementById('<%= RequiredFieldValidator14.ClientID %>').enabled = false;
                                            document.getElementById('<%= RequiredFieldValidator15.ClientID %>').enabled = false;
                                       document.getElementById('<%= RequiredFieldValidator19.ClientID %>').enabled = false;
                                    }
                                    //FOR DRAFT PURPOSES
                                    function disableValidators() {

                                        // Enable the validators associated with the additional fields block
                                           document.getElementById('<%= RequiredFieldValidator50.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator32.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator9.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator29.ClientID%>').enabled = false;


                                           document.getElementById('<%= RequiredFieldValidator10.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator11.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator12.ClientID %>').enabled = false;

                                           document.getElementById('<%= RequiredFieldValidator14.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator15.ClientID %>').enabled = false;
                                           document.getElementById('<%= RequiredFieldValidator19.ClientID %>').enabled = false;

                                        // disable the validators associated for the one-way block
                                        document.getElementById('<%= RequiredFieldValidator13.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator4.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator6.ClientID %>').enabled = false;
                                        // disable the validators associated with the roundtrip block
                                        document.getElementById('<%= RequiredFieldValidator17.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator18.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator8.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator7.ClientID %>').enabled = false;
                                        // DISABLE the validators associated with the multiple block
                                        document.getElementById('<%= RequiredFieldValidator21.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator22.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator26.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator23.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator24.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator28.ClientID %>').enabled = false;

                                        //EMPLOYEE INFORMATION
                                        document.getElementById('<%= RequiredFieldValidator30.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator2.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator88.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator5.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator20.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator3.ClientID %>').enabled = false;
					                    document.getElementById('<%= RequiredFieldValidator16.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator57.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator25.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator27.ClientID %>').enabled = false;
                                        document.getElementById('<%= RequiredFieldValidator29.ClientID %>').enabled = false;


                                       }
                                </script>


<script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <style>
        .txtBox{
            Width: 260px;
            margin-left:80px;
        }
        .auto-style1 {
            margin-left: 97px;
        }
        .auto-style3 {
            margin-left: 76px;
        }
        .auto-style4 {
            margin-left: 83px;
        }
        .auto-style5 {
            margin-left: 152px;
        }
        .auto-style6 {
            margin-left: 106px;
        }
        .auto-style7 {
            margin-left: 88px;
        }
        .auto-style9 {
            margin-left: 65px;
        }
        .auto-style10 {
            margin-left: 114px;
        }
        .auto-style11 {
            margin-left: 78px;
        }
        .auto-style12 {
            margin-left: 92px;
        }
        .auto-style13 {
            margin-left: 75px;
        }
        .required{
            color:red;
            font-size:14px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
           <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/travelRequests.png" style="width: 250px;" alt="logo.png">

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
                                                        <h5 style="color:white">Domestic Travel Request Form</h5>
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
                                                                <br /> <br />
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
                                                                <asp:TextBox ID="employeeEmail" runat="server" Width="300px" Style="margin-left: 50px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeEmail"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label16" runat="server" Text="Birthdate" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeeBdate" runat="server" TextMode="Date" Width="150px" Style="margin-left: 25px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeBdate"></asp:RequiredFieldValidator>


                                                            </div>

                                                            <!--TRAVEL DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                <asp:DropDownList ID="employeePurpose" runat="server" CssClass="m-l-50" Width="343px" onchange="showHideOthers(this)">
                                                                    <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True" />
                                                                    <asp:ListItem Text="Strategic Planning" Value="Strategic Planning" />
                                                                    <asp:ListItem Text="Facility Visit" Value="Client Meeting" />
                                                                    <asp:ListItem Text="Training" Value="Training" />
                                                                    <asp:ListItem Text="Audit" Value="Audit" />
                                                                    <asp:ListItem Text="Client Visit" Value="Client Visit" />
                                                                    <asp:ListItem Text="Others" Value="Others" />
                                                                </asp:DropDownList>                                                                   
                                                                <br /><br />
                                                                <asp:Label ID="Label14" runat="server" Text="Others, please specify... " Style="display: none;"></asp:Label>
                                                                <asp:TextBox ID="otherspecified" runat="server" Width="320px" TextMode="MultiLine" Style="margin-left: 170px; display: none"></asp:TextBox>
                                                            </div>
                                        <div class="card-block">
                                            <asp:Label ID="Label10" runat="server" Text="Flight Options"></asp:Label>
                                            <asp:DropDownList ID="flightOptions" runat="server" CssClass="m-l-50" Width="343px" onchange="flightSelection()" Style="margin-left: 70px">
                                                <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True" />
                                                <asp:ListItem Text="One Way" Value="One Way" />
                                                <asp:ListItem Text="Round trip" Value="Roundtrip" />
                                                <asp:ListItem Text="Multiple Destinations" Value="multiple" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                            <asp:Label ID="Label12" runat="server" Text="Departing From"></asp:Label>
                                            <asp:TextBox ID="onewayFrom" runat="server"  Width="260px" Style="margin-left: 60px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="onewayFrom"></asp:RequiredFieldValidator>

                                            <asp:Label ID="Label21" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                            <asp:TextBox ID="onewayTo" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="onewayTo"></asp:RequiredFieldValidator>
                                                    
                                            <asp:Label ID="Label4" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                            <asp:TextBox ID="onewayDate" TextMode="Date" runat="server"  Width="120px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="onewayDate"></asp:RequiredFieldValidator>
                                        
                                        </div>
                                        <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                            <asp:Label ID="Label22" runat="server" Text="Departing From"></asp:Label>
                                            <asp:TextBox ID="round1From" runat="server"  Width="260px" Style="margin-left: 60px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="round1From"></asp:RequiredFieldValidator>

                                            <asp:Label ID="Label23" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                            <asp:TextBox ID="round1To" runat="server" Width="260px" Style="margin-left: 40px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="round1To"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="Label5" runat="server" Text="Departure Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="roundDepart" TextMode="Date" runat="server"  Width="120px" Style="margin-left: 30px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="roundDepart"></asp:RequiredFieldValidator>

                                            <asp:Label ID="Label6" runat="server" Text="Return Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="roundReturn" TextMode="Date" runat="server"  Width="120px" Style="margin-left: 30px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="roundReturn"></asp:RequiredFieldValidator>

                                            <br />
                                        </div>
                                          <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                            <!-- Multiple destinations flight input fields -->
        
                                                <div id="destination1">
                                                    <!--FIRST DESTINATION-->
                                                    <asp:Label ID="Label34" runat="server" Text="1st Destination:"></asp:Label><br />
                                                    <asp:Label ID="Label26" runat="server" Text="1. Departing From" Style="margin-left: 10px"></asp:Label>
                                                    <asp:TextBox ID="TextBox7" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox7"></asp:RequiredFieldValidator>
                                                                                                     
                                                    <asp:Label ID="Label27" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                    <asp:TextBox ID="TextBox8" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox8"></asp:RequiredFieldValidator>
                                                   
                                                    <asp:Label ID="Label31" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox12" TextMode="Date" runat="server"  Width="100px" Style="margin-left: 20px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox12"></asp:RequiredFieldValidator>
                                                 </div>   <br />
                                                <div id="destination2">
                                                    <!--SECOND DESTINATION-->
                                                    <asp:Label ID="Label35" runat="server" Text="2nd Destination:"></asp:Label> <br />
                                                    <asp:Label ID="Label28" runat="server" Text="2. Departing From" Style="margin-left: 10px"></asp:Label>
                                                    <asp:TextBox ID="TextBox9" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox9"></asp:RequiredFieldValidator>

                                                    <asp:Label ID="Label29" runat="server" Text=" To" Style="padding-left: 50px"></asp:Label>
                                                    <asp:TextBox ID="TextBox10" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox10"></asp:RequiredFieldValidator>

                                                    <asp:Label ID="Label33" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox14" TextMode="Date" runat="server" Width="100px" Style="margin-left: 20px"></asp:TextBox>
                                                    <asp:Button runat="server" ID="add3rd" class="btn btn-primary" Text="+"  OnClientClick="add3rd(); return false;" CausesValidation="False"/>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox14"></asp:RequiredFieldValidator>

                                                </div>                                                   
                                            </div>
                                                             <div id="destination3" runat="server" class="card-block" style="display:none">
                                                                 <!--THIRD DESTINATION-->
                                                                <asp:Label ID="Label36" runat="server" Text="3rd Destination:"></asp:Label><br />
                                                                <asp:Label ID="Label37" runat="server" Text="3. Departing From" Style="margin-left: 10px"></asp:Label>
                                                                <asp:TextBox ID="TextBox15" runat="server" Width="260px" CssClass="auto-style11"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox15"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label39" runat="server" Text= "To" Style="padding-left: 50px"></asp:Label>
                                                                <asp:TextBox ID="TextBox17" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox17"></asp:RequiredFieldValidator>
                                                               
                                                                 <asp:Label ID="Label40" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                <asp:TextBox ID="TextBox18" TextMode="Date" runat="server"  Width="100px" Style="margin-left: 20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox18"></asp:RequiredFieldValidator>
                                                                 <asp:Button runat="server" ID="add4th" class="btn btn-primary" Text="+" OnClientClick="add4th();  return false;"  CausesValidation="False"/>

                                                              </div>

                                                             <div id="destination4" style="display:none" runat="server" class="card-block"> <br />
                                                                            <!--FOURTH DESTINATION-->
                                                                            <asp:Label ID="Label51" runat="server" Text="4th Destination:"></asp:Label><br />
                                                                            <asp:Label ID="Label52" runat="server" Text="4. Departing From" Style="margin-left: 10px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox27" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox27"></asp:RequiredFieldValidator>

                                                                            <asp:Label ID="Label54" runat="server" Text=" To" Style="padding-left: 50px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox29" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox29"></asp:RequiredFieldValidator>
                                                                            
                                                                 <asp:Label ID="Label55" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox30" TextMode="Date" runat="server"  Width="100px" Style="margin-left: 20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox30"></asp:RequiredFieldValidator>
                                                                 <asp:Button runat="server" ID="add5th" class="btn btn-primary" Text="+" OnClientClick="add5th();  return false;"  CausesValidation="False" />
                                                               
                                                                 </div>              
                                                    
                                                             <div id="destination5" style="display:none" runat="server" class="card-block"> <br />
                                                                        <!--FIFTH DESTINATION-->
                                                                        <asp:Label ID="Label41" runat="server" Text="5th Destination:"></asp:Label><br />
                                                                        <asp:Label ID="Label42" runat="server" Text="5. Departing From" Style="margin-left: 10px"></asp:Label>
                                                                        <asp:TextBox ID="TextBox19" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox19"></asp:RequiredFieldValidator>

                                                                        <asp:Label ID="Label44" runat="server" Text=" To" Style="padding-left: 50px"></asp:Label>
                                                                        <asp:TextBox ID="TextBox21" runat="server" Width="260px" Style="margin-left: 50px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox21"></asp:RequiredFieldValidator>

                                                                        <asp:Label ID="Label45" runat="server" Text="Date" Style="margin-left: 30px" ></asp:Label>
                                                                        <asp:TextBox ID="TextBox22" TextMode="Date" runat="server"  Width="100px" Style="margin-left: 20px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="TextBox22"></asp:RequiredFieldValidator>

                                                                </div>   
                                                    <div class="card-block" style="display:none" id="additionalFields" runat="server"> <hr /> 


                                                    </div>


                                                 
                                        <!--END OF FLIGHT INFORMATION-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                            </div>
                                                            <div class="card-block" id="uploadBlock" runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="Attach Email Approval"></asp:Label>
                                                               <asp:FileUpload ID="employeeUpload" type="file" runat="server" CssClass="auto-style12" Width="348px" />
                                                                <asp:Button class="form-control btn btn-primary btn-sm"  ID="Button1" runat="server" Text="Upload" OnClick="btnUpload_Click" Width="150px" style="margin-left:10px" CausesValidation="False" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeUpload"></asp:RequiredFieldValidator>

                                                            </div>
                                                                <div class="col-md-5" style="display:none" id="pdfBlock" runat="server">
                                                                  <asp:Label ID="Label17" runat="server" Text="Manager Email Approval"></asp:Label>     <br />  <br />                                                           
                                                                        <asp:Button runat="server" class="btn btn-danger" Text="Delete" ID="Delete" OnClick="reUpload_Click" CausesValidation="False" /> 
                                                                    <br /> <br />
                                                                        <iframe id="pdfViewer" runat="server" style="width:100%; height:600px;margin-left:150px" frameborder="0"></iframe>
                                                            
                                                            <div class="card-block">
                                                                 <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                                <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="auto-style10" TextMode="MultiLine" Height="91px"></asp:TextBox> 
                                                                
                                                            </div>
                                                 </div>


                                           </div>
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit" ID="submitRequestbtn" OnClick="submitRequestbtn_Click"/>
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Save as Draft" ID="saveAsDraft" OnClientClick = "disableValidators();" OnClick="saveAsDraft_Click" />

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