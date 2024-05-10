<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="internationalRequestDetails.aspx.cs" Inherits="TravelDesk.Employee.internationalRequestDetails" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
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
        .tracker {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 50px auto;
            width: 80%;
        }

        .stage {
            text-align: center;
            position: relative;
        }

        .circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            background-color: #ccc; /* Default color for uncompleted stages */
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 18px;
            position: relative;
            z-index: 1;
        }

        .completed {
            background-color: #33D176; /* Green color for completed stages */

        }

        .text {
            margin-top: 10px;
        }

        .line {
            flex: 1;
           background-color: #ccc;
            height: 5px; /* Adjust line thickness */
        }

        .line.completed {
            background-color: #33D176; /* Change color for completed line */
        }
        .circle:hover::after {
            content: attr(data-hover-message); /* Display the hover message */
            position: absolute;
             top: -40px; /* Position the hover message above the circle */
            left: calc(50% + 5px); /* Position the hover message to the right of the circle */
            padding: 5px;
            text-align:center;
            width: 200px;
            background-color:lightgrey; /* Background color of the hover message */
            color: black; /* Text color of the hover message */
            font-size: 12px;
            border-radius: 5px;
            z-index: 999; /* Ensure the hover message appears above other elements */
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
                                         <div class="page-body" style="color:black">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <!-- Tab variant tab card start -->
                                                        <div class="card" style="background-image:url('images/bgImage.jpg')">
                                                            <div class="card-header" style="background-color:#09426a">
                                                                <h5 style="color:white">International Travel Request</h5>
                                                            </div>
                                                            <div class="card-block tab-icon">
                                                                    <div class="col-lg-12 ">
<%--                                                                        <div class="sub-title">Tab With Icon</div>--%>
                                                                        <!-- Nav tabs -->
                                                                        <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                         <li class="nav-item">
                                                                                <a class="nav-link active" data-toggle="tab" href="#requestTracking" role="tab"><i class="icofont icofont-ui-message"></i>Request Tracking</a>
                                                                                <div class="slide"></div>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#requestDetails" role="tab"><i class="icofont icofont-home"></i>Request Details</a>
                                                                                <div class="slide"></div>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#managerApproval" role="tab"><i class="icofont icofont-ui-user "></i>Attached Files</a>
                                                                                <div class="slide"></div>
                                                                            </li>
   
                                                                            
                                                                        </ul>
                                                                        <!-- Tab panes -->
                                                                        <div class="tab-content card-block">
                                                                            <div class="tab-pane active" id="requestTracking" role="tabpanel">
                                                                                <asp:Label runat="server" style="font-size:16px" >Current Status:
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="currentStatus" style="color:#4CAF50;font-size:18px;margin-left:10px"></asp:Label>
                                                                                <div class="tracker">
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="requestSubmittedCircle" data-hover-message="Requests that has been submitted and auto-approved."><span>1</span></div>
                                                                                        <div class="text">Approved</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="processingCircle" data-hover-message="Requests that has been accepted for travel arrangement processing."><span>2</span></div>
                                                                                        <div class="text">Processing</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="arrangedCircle" data-hover-message="Requests that completed the travel arrangement process."><span>3</span></div>
                                                                                        <div class="text">Arranged</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="completedCircle" data-hover-message="Requests that completed the travel arrangement and billing process"><span>4</span></div>
                                                                                        <div class="text">Completed</div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane" id="requestDetails" role="tabpanel">

                                                                                <!--EMPLOYEE DETAILS-->
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                                                </div>
                                                                                <div class="card-block">
                                                                                <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                                                <asp:TextBox  ID="homeFacility" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                               
                                                                                    <asp:Label ID="Label1" runat="server" Text="Project Code"  Style="margin-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeProjCode" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                        
                                                                                    </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                                    <asp:TextBox ID="employeeID" runat="server" style="margin-left:20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                               

                                                                                    <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="80px"></asp:TextBox>
                                                                
                                                                                    <asp:Label ID="Label5" runat="server" Text="Department Unit" style="padding-left:50px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeDU" runat="server" Width="200px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                          
                                                                                    </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                                                                                    <asp:TextBox ID="employeeFName" runat="server" Width="300px" Style="margin-left: 30px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label19" runat="server" Text="Middle Name" Style="padding-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeMName" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label20" runat="server" Text="Last Name" Style="padding-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeLName" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="card-block">

                                                                                    <asp:Label ID="Label3" runat="server" Text="Mobile"></asp:Label>
                                                                                    <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                
                                                                                    <asp:Label ID="Label46" runat="server" Text="Email" Style="padding-left: 60px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeEmail" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false" ></asp:TextBox>

                                                                                    <asp:Label ID="Label16" runat="server" Text="Birthdate" Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeBdate" runat="server" Width="150px" Style="margin-left: 30px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>



                                                                                </div>

                                                                                <!--TRAVEL DETAILS-->
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                                                </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                                    <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="343px" ></asp:TextBox>                                                              
                                                               
                                                                                </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Flight Options"></asp:Label>
                                                                                    <asp:TextBox ID="flightOptions" runat="server"  Width="343px" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        

                                                                                </div>
                                                                                <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                    <asp:TextBox ID="onewayFrom" runat="server"  Width="260px"  Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>

                                                                                    <asp:Label ID="Label21" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="onewayTo" runat="server" Width="260px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label4" runat="server" Text="Date" Style="padding-left: 30px"></asp:Label>
                                                                                    <asp:TextBox ID="onewayDate" runat="server" Width="260px" Style="margin-left: 80px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                </div>
                                                                                <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                                                                    <asp:Label ID="Label22" runat="server" Text="Departing From"></asp:Label>
                                                                                    <asp:TextBox ID="round1From" runat="server"  Width="260px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>

                                                                                    <asp:Label ID="Label23" runat="server" Text="To" Style="padding-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="round1To" runat="server" Width="200px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label24" runat="server" Text="Departure Date" Style="margin-left: 30px"></asp:Label>
                                                                                    <asp:TextBox ID="round2departure" runat="server"  Width="150px" Style="margin-left: 30px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label25" runat="server" Text="Return Date" Style="padding-left: 30px"></asp:Label>
                                                                                    <asp:TextBox ID="round2return" runat="server" Width="150px" Style="margin-left: 30px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                  <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                                                                    <!-- Multiple destinations flight input fields -->
        
                                                                                        <div id="destination1">
                                                                                            <!--FIRST DESTINATION-->
                                                                                            <asp:Label ID="Label34" runat="server" Text="1st Destination:"></asp:Label><br />
                                                                                            <asp:Label ID="Label26" runat="server" Text="1. Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox7" runat="server" Enabled="false" Style="margin-left: 20px; border-radius: 5px" Width="260px" CssClass="textboxes" ></asp:TextBox>
                                                   
                                                                                            <asp:Label ID="Label27" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox8" runat="server" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                   
                                                                                            <asp:Label ID="Label31" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox12"  runat="server" Enabled="false" CssClass="textboxes" Style="margin-left: 20px; border-radius: 5px"  Width="100px"></asp:TextBox>
                                                                                         </div>   <br />
                                                                                        <div id="destination2">
                                                                                            <!--SECOND DESTINATION-->
                                                                                            <asp:Label ID="Label35" runat="server" Text="2nd Destination:"></asp:Label><br />
                                                                                            <asp:Label ID="Label28" runat="server" Text="2. Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox9" runat="server" Enabled="false" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes" ></asp:TextBox>

                                                                                            <asp:Label ID="Label29" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox10" runat="server" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                            <asp:Label ID="Label33" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox14" runat="server" Enabled="false" Width="100px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes" ></asp:TextBox>

                                                                                        </div>                                                   
                                                                                    </div>
                                                                                <div class="card-block" style="display:none" id="additionalFields" runat="server">
                                                                                         <div id="destination3">
                                                                                             <!--THIRD DESTINATION-->
                                                                                            <asp:Label ID="Label36" runat="server" Text="3rd Destination:"></asp:Label><br />
                                                                                            <asp:Label ID="Label37" runat="server" Text="3. Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox15" runat="server" Enabled="false" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes" ></asp:TextBox>

                                                                                            <asp:Label ID="Label39" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox17" runat="server" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                            <asp:Label ID="Label40" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                            <asp:TextBox ID="TextBox18" runat="server"  Enabled="false" CssClass="textboxes" Style="margin-left: 20px; border-radius: 5px" Width="100px"></asp:TextBox>

                                                                                            </div> <br />
                                                                                         <div id="destination4" style="display:none" runat="server">
                                                                                                        <!--FOURTH DESTINATION-->
                                                                                                        <asp:Label ID="Label51" runat="server" Text="4th Destination:"></asp:Label><br />
                                                                                                        <asp:Label ID="Label52" runat="server" Text="4. Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox27" Enabled="false" runat="server" Style="margin-left: 20px; border-radius: 5px" Width="260px" CssClass="textboxes" ></asp:TextBox>


                                                                                                        <asp:Label ID="Label54" runat="server" Text="To" Style="padding-left: 50px"></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox29" runat="server" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                        <asp:Label ID="Label55" runat="server" Text="Date" Style="margin-left: 30px"></asp:Label>
                                                                                                        <asp:TextBox ID="TextBox30" Enabled="false" runat="server" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Width="100px"></asp:TextBox>

                                                                                            </div>         <br />                                    
                                                                                         <div id="destination5" style="display:none" runat="server">
                                                                                                    <!--FIFTH DESTINATION-->
                                                                                                    <asp:Label ID="Label41" runat="server" Text="5th Destination:"></asp:Label><br />
                                                                                                    <asp:Label ID="Label42" runat="server" Text="5. Departing From" Style="margin-left: 60px"></asp:Label>
                                                                                                    <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes" ></asp:TextBox>

                                                                                                    <asp:Label ID="Label44" runat="server" Text="To" Style="padding-left: 60px"></asp:Label>
                                                                                                    <asp:TextBox ID="TextBox21" runat="server" Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                    <asp:Label ID="Label45" runat="server" Text="Date" Style="margin-left: 40px"></asp:Label>
                                                                                                    <asp:TextBox ID="TextBox22" runat="server" Enabled="false" Width="100px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes" ></asp:TextBox>

                                                                                            </div>   

                                                                                </div>  
                                                                                  <!--END OF FLIGHT INFORMATION-->
                                                                                <div class="card-block">
                                                                                     <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                                                    <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="textboxes"  TextMode="MultiLine" Height="91px" Enabled="false"></asp:TextBox> 
                                                                                </div>


                                                                            </div>
                                                                            <div class="tab-pane" id="managerApproval" role="tabpanel">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                <div class="card-block" id="uploadBlock" runat="server">
                                                                                                    <asp:Label ID="Label6" runat="server" Text="Email Approval" Style="margin-left: 20px"></asp:Label> <br /> <br />
                                                                                                    <iframe id="pdfViewer"  runat="server" style="width:100%; height:600px" frameborder="0"></iframe>
                                                                                                </div>   
                                                                                            </div>
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" id="Div1" runat="server">
                                                                                                    <asp:Label ID="Label9" runat="server" Text="Scanned Passport" Style="margin-left: 20px"></asp:Label> <br /> <br />
                                                                                                     <iframe id="passportViewer"  runat="server" style="width:100%; height:600px" frameborder="0"></iframe>
                                                                                                </div>   
                                                                                            </div>
                                                                                        </div>

                                                                              
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                          </div>
                                                        </div>
                                                        <!-- Tab variant tab card start -->
                                                    </div>
                                                </div>
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
