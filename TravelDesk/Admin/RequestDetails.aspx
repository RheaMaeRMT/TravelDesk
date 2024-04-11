<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestDetails.aspx.cs" Inherits="TravelDesk.Admin.RequestDetails" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
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
        .textboxes{
            color:black;
            background-color:transparent;
           
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
            <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div class="page-header" style="background-color:#09426a">
                          <div class="page-block">
                              <div class="row align-items-center">
                                  <div class="col-md-8">
                                      <div class="page-header-title">
                                          <h5 class="m-b-10">TRAVEL REQUEST - DOMESTIC</h5>
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
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                         <div class="page-body" style="color:black">
                                                <div class="card" style="color:black;">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Domestic Travel Request Form</h5>
                                                    </div>                                                            
                                                            <div class="card-block">
                                                            <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                            <asp:TextBox  ID="homeFacility" runat="server" Width="345px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                               
                                                            </div>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                                                                <asp:TextBox ID="employeeFName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label19" runat="server" Text="Middle Name" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeeMName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label20" runat="server" Text="Last Name" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeeLName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label1" runat="server" Text="Project Code"></asp:Label>
                                                                <asp:TextBox ID="employeeProjCode" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label3" runat="server" Text="Mobile Number" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>


                                                                <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:60px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px"></asp:TextBox>

                                                            </div>

                                                            <!--TRAVEL DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="343px" ></asp:TextBox>                                                              
                                                                <asp:Label ID="Label6" runat="server" Text="Date of Departure"  Style="margin-left: 60px"></asp:Label>
                                                                <asp:TextBox ID="employeeDepartureDate" runat="server" Width="200px" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label5" runat="server" Text="Date of Return" Style="padding-left: 60px"></asp:Label>
                                                                <asp:TextBox ID="employeeArrivalDate" runat="server" Width="200px" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                            </div>

                                                            <div class="card-block">
                                                                <asp:Label ID="Label4" runat="server" Text="Departing From"></asp:Label>
                                                                <asp:TextBox ID="employeeFrom" runat="server" Width="343px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label9" runat="server" Text="Arriving To" Style="padding-left:60px"></asp:Label>
                                                                <asp:TextBox ID="employeeTo" runat="server" Width="260px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                            </div>

                                                            <div class="card-block">



                                                            </div>

                                        <!--FLIGHT INFORMATION-->

                                        <div class="card-block">
                                            <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Flight Information</p>
                                        </div>
                                        <div class="card-block">
                                            <asp:Label ID="Label10" runat="server" Text="Flight Options"></asp:Label>
                                            <asp:TextBox ID="flightOptions" runat="server"  Width="343px" Style="margin-left: 70px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        

                                        </div>
                                        <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                            <asp:Label ID="Label12" runat="server" Text="Departing From"></asp:Label>
                                            <asp:TextBox ID="onewayFrom" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                            <asp:Label ID="Label21" runat="server" Text="Departing To" Style="padding-left: 150px"></asp:Label>
                                            <asp:TextBox ID="onewayTo" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                            <asp:Label ID="Label22" runat="server" Text="1. Departing From"></asp:Label>
                                            <asp:TextBox ID="round1From" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                            <asp:Label ID="Label23" runat="server" Text="1. Departing To" Style="padding-left: 150px"></asp:Label>
                                            <asp:TextBox ID="round1To" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                            <br />
                                            <br />
                                            <asp:Label ID="Label24" runat="server" Text="2. Departing From"></asp:Label>
                                            <asp:TextBox ID="round2From" runat="server"  Width="260px" CssClass="auto-style11"></asp:TextBox>

                                            <asp:Label ID="Label25" runat="server" Text="2. Departing To" Style="padding-left: 150px"></asp:Label>
                                            <asp:TextBox ID="round2To" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                        </div>
                                          <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                            <!-- Multiple destinations flight input fields -->
        
                                                <div id="destination1">
                                                    <!--FIRST DESTINATION-->
                                                    <asp:Label ID="Label34" runat="server" Text="1st Destination:"></asp:Label><br />
                                                    <asp:Label ID="Label26" runat="server" Text="1. Departing From"></asp:Label>
                                                    <asp:TextBox ID="TextBox7" runat="server" Enabled="false"  Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                   
                                                    <asp:Label ID="Label30" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox11"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>
                                                    
                                                    <asp:Label ID="Label27" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                    <asp:TextBox ID="TextBox8" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="Label31" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox12"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>
                                                 </div>   <br />
                                                <div id="destination2">
                                                    <!--SECOND DESTINATION-->
                                                    <asp:Label ID="Label35" runat="server" Text="2nd Destination:"></asp:Label><br />
                                                    <asp:Label ID="Label28" runat="server" Text="2. Departing From"></asp:Label>
                                                    <asp:TextBox ID="TextBox9" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                    <asp:Label ID="Label32" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox13"  runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                    <asp:Label ID="Label29" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                    <asp:TextBox ID="TextBox10" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="Label33" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                    <asp:TextBox ID="TextBox14" runat="server" Enabled="false" Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                </div>                                                   
                                            </div>
                                                    <div class="card-block" style="display:none" id="additionalFields" runat="server"> <hr />
                                                             <div id="destination3">
                                                                 <!--THIRD DESTINATION-->
                                                                <asp:Label ID="Label36" runat="server" Text="3rd Destination:"></asp:Label><br />
                                                                <asp:Label ID="Label37" runat="server" Text="3. Departing From"></asp:Label>
                                                                <asp:TextBox ID="TextBox15" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                                <asp:Label ID="Label38" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                <asp:TextBox ID="TextBox16" runat="server" Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                <asp:Label ID="Label39" runat="server" Text="Departing To" Style="padding-left: 50px"></asp:Label>
                                                                <asp:TextBox ID="TextBox17" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label40" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                <asp:TextBox ID="TextBox18" runat="server"  Enabled="false" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                </div>
                                                             <div id="destination4" style="display:none" runat="server">
                                                                            <!--FOURTH DESTINATION-->
                                                                            <asp:Label ID="Label51" runat="server" Text="4th Destination:"></asp:Label><br />
                                                                            <asp:Label ID="Label52" runat="server" Text="4. Departing From"></asp:Label>
                                                                            <asp:TextBox ID="TextBox27" Enabled="false" runat="server"  Width="260px" CssClass="textboxes" ></asp:TextBox>

                                                                            <asp:Label ID="Label53" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox28" Enabled="false" runat="server" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                            <asp:Label ID="Label54" runat="server" Text="Departing To" Style="padding-left: 60px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox29" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                            <asp:Label ID="Label55" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                            <asp:TextBox ID="TextBox30" Enabled="false" runat="server" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                </div>                                             
                                                             <div id="destination5" style="display:none" runat="server">
                                                                        <!--FIFTH DESTINATION-->
                                                                        <asp:Label ID="Label41" runat="server" Text="5th Destination:"></asp:Label><br />
                                                                        <asp:Label ID="Label42" runat="server" Text="5. Departing From"></asp:Label>
                                                                        <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                                        <asp:Label ID="Label43" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                        <asp:TextBox ID="TextBox20" runat="server" Enabled="false"  Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                                        <asp:Label ID="Label44" runat="server" Text="Departing To" Style="padding-left: 60px"></asp:Label>
                                                                        <asp:TextBox ID="TextBox21" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                        <asp:Label ID="Label45" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                        <asp:TextBox ID="TextBox22" runat="server" Enabled="false" Width="100px" CssClass="textboxes" ></asp:TextBox>

                                                                </div>   

                                                    </div>


                                                 
                                        <!--END OF FLIGHT INFORMATION-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                            </div>
                                                            <div class="card-block" id="approvalBlock" runat="server">
                                                                <!-- manager name should auto-populate based on the manager assigned of the employees department -->
                                                                <asp:Label ID="Label17" runat="server" Text="Manager Name" style="padding-left:20px" ></asp:Label>
                                                                <asp:TextBox ID="employeeManager" runat="server" Width="343px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block" id="uploadBlock" style="display:none" runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="Approval Proof"></asp:Label>
                                                                <iframe id="pdfViewer"  runat="server" style="width:100%; display:none; height:600px" frameborder="0"></iframe>

                                                            </div> 
                                                                   
                                                            
                                                            <div class="card-block">
                                                                 <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                                <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="textboxes"  TextMode="MultiLine" Height="91px" Enabled="false"></asp:TextBox> 
                                                            </div>
                                                 
                                           </div>
                                                <asp:Button runat="server" class="btn btn-primary" Text="Process Request" ID="processRequest" OnClick="processRequest_Click"/>
                                    
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
