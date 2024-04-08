<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelArrangements.aspx.cs" Inherits="TravelDesk.Admin.TravelArrangements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript">
        function showModal() {
            $('#requestModal').modal('show');
            return false; // Prevents the default behavior of the button click event
        }


        function accomodationOptions() {
            var accomodations = document.getElementById('<%= accomodations.ClientID %>');
            var selectedOption = accomodations ? accomodations.options[accomodations.selectedIndex].value : null;


            if (selectedOption === "Hotel Accomodation") {
                document.getElementById('<%= hotelAccomodations.ClientID %>').style.display = 'block';
            } else {
                document.getElementById('<%= hotelAccomodations.ClientID %>').style.display = 'none';


            }
        }
        function add2Route(button) {
            document.getElementById('<%= additional2routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add2nd.ClientID %>').style.display = 'none';

        }
        function add3Route(button) {
            document.getElementById('<%= additional3routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add3rd.ClientID %>').style.display = 'none';

        }
        function add4Route(button) {
            document.getElementById('<%= additional4routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add4th.ClientID %>').style.display = 'none';

        }
        function add5Route(button) {
            document.getElementById('<%= additional5routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add5th.ClientID %>').style.display = 'none';

        }
        function add2Transfer(button) {
            document.getElementById('<%= transfers2.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button1.ClientID %>').style.display = 'none';

        } function add3Transfer(button) {
            document.getElementById('<%= transfers3.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button2.ClientID %>').style.display = 'none';

        } function add4Transfer(button) {
            document.getElementById('<%= transfers4.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button3.ClientID %>').style.display = 'none';

        } function add5Transfer(button) {
            document.getElementById('<%= transfers5.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button4.ClientID %>').style.display = 'none';

        }





    </script>
    <style>
        .checkbox-list {
    padding-bottom: 20px; /* Add space at the bottom of the checkbox list */
}

.checkbox-list label {    margin-bottom: 10px; /* Add space between checkbox items */
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div class="page-header" style="background-color:#09426a">
                          <div class="page-block">
                              <div class="row align-items-center">
                                  <div class="col-md-8">
                                      <div class="page-header-title">
                                          <h5 class="m-b-10">TRAVEL ARRANGEMENT</h5>
                                      </div>
                                  </div>
                                  <div class="col-md-4">
                                      <ul class="breadcrumb-title">
                                          <li class="breadcrumb-item">
                                              <a href="index.html"> <i class="fa fa-home"></i> </a>
                                          </li>
                                          <li class="breadcrumb-item"><a href="EmployeeDashboard.aspx">Dashboard</a>
                                          </li>
                                      </ul>
                                  </div>
                              </div>
                          </div>
                      </div>
                      <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body" >
                                <div class="page-wrapper" >
                                    <!-- Page-body start -->
                                         <div class="page-body" style="color:black;font-size:16px;">
                                                <div class="card" style="color:black;background-color:white">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Travel Arrangement Form</h5>
                                                    </div>      
                                                                                     
                                                    <div class="card-block">
                                             <asp:Button runat="server" class="btn btn-primary" Text="Open Request" ID="openRequest" OnClientClick="return showModal();" />

                                                    </div>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label7" runat="server" Text="Traveller Name"></asp:Label>
                                                                <asp:TextBox ID="employeeName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID"  style="margin-left:40px;"></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:60px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px"></asp:TextBox>
       
                                                                </div>
                                                            <div class="card-block">

                                                                <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                                <asp:TextBox  ID="homeFacility" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                               
                                                                                                                         <asp:Label ID="Label3" runat="server" Text="Mobile Number" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                </div>
                                                            
                                                            <!--ARRANGEMENTS -->
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Accomodations</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label5" runat="server" Text="Accomodations"></asp:Label>
                                                                <asp:DropDownList ID="accomodations" runat="server"  Style="margin-left: 60px" Width="345px" onchange="accomodationOptions()">
                                                                <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True" />
                                                                    <asp:ListItem Value="Hotel Accomodation" Text="Hotel Accomodation"> </asp:ListItem>
                                                                    <asp:ListItem Value="c/o Traveller" Text="c/o Traveller">  </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotelAccomodations" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label1" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotel" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label4" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label9" runat="server" Text="Hotel Duration" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label10" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="durationFrom" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label12" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="durationTo" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Flight Details</p>

                                                            </div>
                                                             <div class="card-block">
                                                                <asp:Label ID="Label16" runat="server" Text="Airline" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="airline" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox> <hr />
                                                             </div> 
                                                                                        <!-- Row start -->
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <asp:Label ID="Label13" runat="server" Text="Flight Schedule" Style="margin-left: 30px;"></asp:Label>
                                                                                                <div class="card-block">
                                                                                                    <asp:Label ID="Label14" runat="server" Text="Travel Route" Style="margin-left: 20px;"></asp:Label>
                                                                                                    <asp:TextBox ID="route1" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="route1Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                     <asp:Button runat="server" ID="add2nd" class="btn btn-primary" Text="+" OnClientClick="add2Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;margin-left:110px" id="additional2routeFields" runat="server">
                                                                                                    <asp:TextBox ID="route2" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="route2Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                     <asp:Button runat="server" ID="add3rd" class="btn btn-primary" Text="+" OnClientClick="add3Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;margin-left:110px" id="additional3routeFields" runat="server">
                                                                                                    <asp:TextBox ID="route3" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="route3Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                     <asp:Button runat="server" ID="add4th" class="btn btn-primary" Text="+" OnClientClick="add4Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;margin-left:110px" id="additional4routeFields" runat="server">
                                                                                                    <asp:TextBox ID="route4" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="route4Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                     <asp:Button runat="server" ID="add5th" class="btn btn-primary" Text="+" OnClientClick="add5Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;margin-left:110px" id="additional5routeFields" runat="server">
                                                                                                    <asp:TextBox ID="TextBox1" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="TextBox2" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 

                                                                                                </div>
                                                                               
                                                                                            </div>
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                    <asp:Label ID="Label15" runat="server" Text="Car/Airport Transfers" Style="margin-left: 30px;"></asp:Label>
                                                                                                 <div class="card-block"  style="margin-left:10px" id="transferInstructions" runat="server">
                                                                                                      <asp:TextBox ID="transfers" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transferDate" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button1" class="btn btn-primary" Text="+" OnClientClick="add2Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers2" runat="server">
                                                                                                      <asp:TextBox ID="transfer2" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer2Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button2" class="btn btn-primary" Text="+" OnClientClick="add3Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers3" runat="server">
                                                                                                      <asp:TextBox ID="transfer3" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer3Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button3" class="btn btn-primary" Text="+" OnClientClick="add4Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers4" runat="server">
                                                                                                      <asp:TextBox ID="transfer4" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="trasnfer4Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button4" class="btn btn-primary" Text="+" OnClientClick="add5Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers5" runat="server">
                                                                                                      <asp:TextBox ID="transfer5" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer5Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                </div>
                                                           
                                                            
                                                                                            </div>
                                                                                        </div>
                                                                                        <!-- Row end -->

                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Others</p>
                                                            </div>
                                                             <div class="row">
                                                                 <div class="col-lg-12 col-xl-6">
                                                                     <div class="card-block" style="margin-left:50px">
                                                                        <asp:Label ID="Label65" runat="server" Text="Travel Requirements"></asp:Label> 
                                                                         <asp:CheckBoxList runat="server" ID="requirements" Height="40px" Width="300px" CellPadding="5" CellSpacing="5">
                                                                             <asp:ListItem Value="Passport"> &nbsp;  &nbsp;  Passport  </asp:ListItem>
                                                                             <asp:ListItem Value="Valid Visa">  &nbsp; &nbsp; Valid Visa </asp:ListItem>
                                                                             <asp:ListItem Value="e-Travel Registration">  &nbsp; &nbsp; eTravel System Registration </asp:ListItem>

                                                                         </asp:CheckBoxList>
                                                                     </div> 

                                                                 </div>
                                                                 <div class="col-lg-12 col-xl-6">
                                                                     <div class="card-block" style="margin-left:50px">
                                                                        <asp:Label ID="Label66" runat="server" Text="Additional Notes"></asp:Label> <br />
                                                                    <asp:TextBox ID="TextBox23" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>

                                                                     </div> 

                                                                  </div>
                                                             </div>

                                           </div>
                                                                <!--MODAL FOR REQUEST DETAILS -->
                                                                <div class="modal fade" id="requestModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog modal-lg" role="document" style="max-width: 1500px;">>
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title">Request Full Details</h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                <!-- Your modal content goes here -->
                                                                                        <div class="card" style="color:black;">
                                                                                            <div class="card-header" style="background-color:#09426a">
                                                                                                <h5 style="color:white"> Travel Request Form</h5>
                                                                                            </div>                                                            
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label17" runat="server" Text="Home Facility"></asp:Label>
                                                                                                        <asp:TextBox  ID="TextBox3" runat="server" Width="345px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                               
                                                                                                    </div>
                                                                                                    <!--EMPLOYEE DETAILS-->
                                                                                                    <div class="card-block">
                                                                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                                                                    </div>
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label18" runat="server" Text="Employee ID" ></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox4" runat="server" style="margin-left:40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                                                                    </div>
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label19" runat="server" Text="First Name"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeFName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                        <asp:Label ID="Label20" runat="server" Text="Middle Name" Style="padding-left: 40px"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeMName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                        <asp:Label ID="Label21" runat="server" Text="Last Name" Style="padding-left: 40px"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeLName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label22" runat="server" Text="Project Code"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeProjCode" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                        <asp:Label ID="Label23" runat="server" Text="Mobile Number" Style="padding-left: 40px"></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox5" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>


                                                                                                        <asp:Label ID="Label24" runat="server" Text="Level"  style="padding-left:60px"></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox6" runat="server"   Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px"></asp:TextBox>

                                                                                                    </div>

                                                                                                    <!--TRAVEL DETAILS-->
                                                                                                    <div class="card-block">
                                                                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                                                                    </div>
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label25" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                                                        <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="343px" ></asp:TextBox>                                                              
                                                                                                        <asp:Label ID="Label26" runat="server" Text="Date of Departure"  Style="margin-left: 60px"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeDepartureDate" runat="server" Width="200px" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                        <asp:Label ID="Label27" runat="server" Text="Date of Return" Style="padding-left: 60px"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeArrivalDate" runat="server" Width="200px" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                    </div>

                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label28" runat="server" Text="Departing From"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeFrom" runat="server" Width="343px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                        <asp:Label ID="Label29" runat="server" Text="Arriving To" Style="padding-left:60px"></asp:Label>
                                                                                                        <asp:TextBox ID="employeeTo" runat="server" Width="260px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                    </div>

                                                                                                    <!--FLIGHT INFORMATION-->
                                                                                                    <div class="card-block">
                                                                                                        <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Flight Information</p>
                                                                                                    </div>
                                                                                                    <div class="card-block">
                                                                                                        <asp:Label ID="Label30" runat="server" Text="Flight Options"></asp:Label>
                                                                                                        <asp:TextBox ID="flightOptions" runat="server"  Width="343px" Style="margin-left: 70px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        

                                                                                                    </div>
                                                                                                    <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                                                                                        <asp:Label ID="Label31" runat="server" Text="Departing From"></asp:Label>
                                                                                                        <asp:TextBox ID="onewayFrom" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                                                                                        <asp:Label ID="Label32" runat="server" Text="Departing To" Style="padding-left: 150px"></asp:Label>
                                                                                                        <asp:TextBox ID="onewayTo" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                                                                                        <asp:Label ID="Label33" runat="server" Text="1. Departing From"></asp:Label>
                                                                                                        <asp:TextBox ID="round1From" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                                                                                        <asp:Label ID="Label34" runat="server" Text="1. Departing To" Style="padding-left: 150px"></asp:Label>
                                                                                                        <asp:TextBox ID="round1To" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                        <br />
                                                                                                        <br />
                                                                                                        <asp:Label ID="Label35" runat="server" Text="2. Departing From"></asp:Label>
                                                                                                        <asp:TextBox ID="round2From" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                                                                                        <asp:Label ID="Label36" runat="server" Text="2. Departing To" Style="padding-left: 150px"></asp:Label>
                                                                                                        <asp:TextBox ID="round2To" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                                                                                        <!-- Multiple destinations flight input fields -->
        
                                                                                                            <div id="destination1">
                                                                                                                <!--FIRST DESTINATION-->
                                                                                                                <asp:Label ID="Label37" runat="server" Text="1st Destination:"></asp:Label><br />
                                                                                                                <asp:Label ID="Label38" runat="server" Text="1. Departing From"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox7" runat="server" Enabled="false"  Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                   
                                                                                                                <asp:Label ID="Label39" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox11"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>
                                                    
                                                                                                                <asp:Label ID="Label40" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox8" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                                <asp:Label ID="Label41" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox12"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>
                                                                                                             </div>   <br />
                                                                                                            <div id="destination2">
                                                                                                                <!--SECOND DESTINATION-->
                                                                                                                <asp:Label ID="Label42" runat="server" Text="2nd Destination:"></asp:Label><br />
                                                                                                                <asp:Label ID="Label43" runat="server" Text="2. Departing From"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox9" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                                                                                <asp:Label ID="Label44" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox13"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                                                <asp:Label ID="Label45" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox10" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                                <asp:Label ID="Label46" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                <asp:TextBox ID="TextBox14" runat="server" Enabled="false" Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                                                                            </div>                                                   
                                                                                                        </div>
                                                                                                    <div class="card-block" style="display:none" id="additionalFields" runat="server"> <hr />
                                                                                                                         <div id="destination3">
                                                                                                                             <!--THIRD DESTINATION-->
                                                                                                                            <asp:Label ID="Label47" runat="server" Text="3rd Destination:"></asp:Label><br />
                                                                                                                            <asp:Label ID="Label48" runat="server" Text="3. Departing From"></asp:Label>
                                                                                                                            <asp:TextBox ID="TextBox15" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                                                                                            <asp:Label ID="Label49" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                            <asp:TextBox ID="TextBox16" runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                                                            <asp:Label ID="Label50" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                                                                                            <asp:TextBox ID="TextBox17" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                                            <asp:Label ID="Label51" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                                            <asp:TextBox ID="TextBox18" runat="server"  Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                                                            </div>
                                                                                                                         <div id="destination4" style="display:none" runat="server">
                                                                                                                                        <!--FOURTH DESTINATION-->
                                                                                                                                        <asp:Label ID="Label52" runat="server" Text="4th Destination:"></asp:Label><br />
                                                                                                                                        <asp:Label ID="Label53" runat="server" Text="4. Departing From"></asp:Label>
                                                                                                                                        <asp:TextBox ID="TextBox27" Enabled="false" runat="server"  Width="260px" CssClass="textboxes" ></asp:TextBox>

                                                                                                                                        <asp:Label ID="Label54" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                                                                                        <asp:TextBox ID="TextBox28" Enabled="false" runat="server" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                                                                        <asp:Label ID="Label55" runat="server" Text="Departing To" Style="padding-left: 60px"></asp:Label>
                                                                                                                                        <asp:TextBox ID="TextBox29" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                                                        <asp:Label ID="Label56" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                                                                                        <asp:TextBox ID="TextBox30" Enabled="false" runat="server" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                                                            </div>                                             
                                                                                                                         <div id="destination5" style="display:none" runat="server">
                                                                                                                                    <!--FIFTH DESTINATION-->
                                                                                                                                    <asp:Label ID="Label57" runat="server" Text="5th Destination:"></asp:Label><br />
                                                                                                                                    <asp:Label ID="Label58" runat="server" Text="5. Departing From"></asp:Label>
                                                                                                                                    <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                                                                                                    <asp:Label ID="Label59" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                                                                                    <asp:TextBox ID="TextBox20" runat="server" Enabled="false"  Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                                                                                                    <asp:Label ID="Label60" runat="server" Text="Departing To" Style="padding-left: 60px"></asp:Label>
                                                                                                                                    <asp:TextBox ID="TextBox21" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:Label ID="Label61" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                                                                                    <asp:TextBox ID="TextBox22" runat="server" Enabled="false" Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                                                                                            </div>   

                                                                                                                </div>           
                                                                                                    <!--END OF FLIGHT INFORMATION-->

                                                                                                    <div class="card-block">
                                                                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                                                                    </div>
                                                                                                    <div class="card-block" id="approvalBlock" runat="server">
                                                                                                        <!-- manager name should auto-populate based on the manager assigned of the employees department -->
                                                                                                        <asp:Label ID="Label62" runat="server" Text="Manager Name" style="padding-left:20px" ></asp:Label>
                                                                                                        <asp:TextBox ID="employeeManager" runat="server" Width="343px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                                    </div>
                                                                                                    <div class="card-block" id="uploadBlock" style="display:none" runat="server">
                                                                                                        <asp:Label ID="Label63" runat="server" Text="Approval Proof"></asp:Label>
                                                                                                        <iframe id="pdfViewer"  runat="server" style="width:100%; display:none; height:600px" frameborder="0"></iframe>

                                                                                                    </div> 
                                                                   
                                                            
                                                                                                    <div class="card-block">
                                                                                                         <asp:Label ID="Label64" runat="server" Text="Remarks"></asp:Label> <br />
                                                                                                        <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="textboxes"  TextMode="MultiLine" Height="91px" Enabled="false"></asp:TextBox> 
                                                                                                    </div>
                                                 
                                                                                   </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit Arrangement" ID="submitArrangement" />
                                    
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
