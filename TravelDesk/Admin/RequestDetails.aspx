<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequestDetails.aspx.cs" Inherits="TravelDesk.Admin.RequestDetails" %>

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
                                                                <asp:Label runat="server" style="color:white; font-size:16px;margin-left:10px;" CssClass="h5" ID="travellerName"></asp:Label>
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
                                                                                <a class="nav-link" data-toggle="tab" href="#managerApproval" role="tab"><i class="icofont icofont-ui-user "></i>Manager Email Approval</a>
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
                                                                                <div class="card-block">
                                                                                    <div class="card-header" style="background-color:#4894f1;border-radius:20px">
                                                                                        <asp:Label runat="server" style="color:white; font-size:16px;margin-left:10px;" CssClass="h7" ID="Label2" Text="Employee Information" ></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <!--EMPLOYEE DETAILS-->
                                                                                <div class="row" style="place-content:center;">
                                                                                    <div class="col-md-4">
                                                                                        <div class="card-block" style="text-align:center;">
                                                                                            <i class="ti-user" style="font-size:20px"></i> <br />
                                                                                            <asp:Label runat="server" ID="empFName" style="font-size:22px;"></asp:Label> <br />
                                                                                            <asp:Label runat="server" style="font-size:15px"> Employee ID:</asp:Label>
                                                                                            <asp:Label runat="server" ID="empID" class="h6"></asp:Label> <br />
                                                                                            <asp:Label runat="server" style="font-size:15px"> Level:</asp:Label>
                                                                                            <asp:Label ID="empLevel" runat="server" class="h6"></asp:Label>
                                                                                        </div>

                                                                                    </div>
                                                                                </div> <br />
                                                                                <div class="row" style="place-content:center">
                                                                                     <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card" style="background-color:#C7E9FE">
                                                                                                         <div class="card-header" style="background-color:#09426a">
                                                                                                 <h5 style="color:white">Contact Information</h5>
                                                                                             </div>
                                                                                             <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                 <i class="ti-email"></i>
                                                                                                 <asp:Label runat="server" style="" > Email:</asp:Label>
                                                                                                  <asp:Label ID="empEmail" class="h6" style="margin-left:18px" runat="server"></asp:Label> <br />
                                                                                                 <i class="ti-mobile"></i>
                                                                                                 <asp:Label runat="server" style="" > Mobile:</asp:Label>
                                                                                                 <asp:Label ID="empMobile" class="h6" style="margin-left:10px" runat="server"></asp:Label> <br />
                                                                                                 <i class="ti-calendar"></i>
                                                                                                 <asp:Label runat="server" style="" > Birthdate:</asp:Label>
                                                                                                 <asp:Label ID="empBdate" class="h6" runat="server"></asp:Label>
                                                                                             </div>
                                                                                         </div>

                                                                                    </div>
                                                                                     <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card" style="background-color:#C7E9FE">
                                                                                                         <div class="card-header" style="background-color:#09426a">
                                                                                                 <h5 style="color:white">Company Information</h5>
                                                                                             </div>
                                                                                             <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                 <i class="ti-home"></i>
                                                                                                 <asp:Label runat="server" style="" > Home Facility:</asp:Label>
                                                                                               <asp:Label runat="server" class="h6" style="margin-left:18px" ID="empFacility"></asp:Label> <br />
                                                                                                 <i class="ti-archive"></i>
                                                                                                 <asp:Label runat="server" style="" > Department Unit:</asp:Label>
                                                                                               <asp:Label runat="server" class="h6" ID="empDeptUnit"></asp:Label> <br />
                                                                                                 <i class="ti-id-badge"></i>
                                                                                                 <asp:Label runat="server" style="" > Project Code:</asp:Label>
                                                                                               <asp:Label runat="server" class="h6" style="margin-left:18px" ID="empCode"></asp:Label>
                                                                                             </div>
                                                                                         </div>

                                                                                    </div>

                                                                                </div> 
                                                                                <div class="card-block">
                                                                                    <div class="card-header" style="background-color:#4894f1;border-radius:20px" >
                                                                                        <asp:Label runat="server" style="color:white; font-size:16px;margin-left:10px;" CssClass="h7" ID="Label1" Text="Travel Request"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                                <!--TRAVEL DETAILS--> <br /> 
                                                                                <div class="card-block" style="margin-left:250px">
                                                                                    <asp:Label ID="Label30" runat="server" Text="Purpose of Travel:"></asp:Label> 
                                                                                    <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 30px; border-radius: 5px;color:black;background-color:transparent;font-size:18px" BorderColor="Transparent" Enabled="false" Width="300px" ></asp:TextBox>     <br />
                                                                                    <asp:Label ID="Label14" runat="server" Text="Route Destination:"></asp:Label> 
                                                                                </div> 
                                                                               
                                                                                <center>
  
                                                                                <div class="card-block" style="background-color:#C7E9FE;text-align:center;border-radius:10px;width:fit-content;color:black;"> <br />
                                                                                  <div class="card-block" style="background-color:#C7E9FE;text-align:center;border-radius:10px;width:fit-content;color:black;">
                                                                                   <img src="/images/airplane.png" style="width: 50px" alt="airplane.png">
                                                                                   <asp:TextBox ID="flightOptions" runat="server"  Width="343px"  Style="border-radius: 5px;color:black;background-color:transparent;font-size:18px" BorderColor="Transparent"   Enabled="false"></asp:TextBox>                                        
                                                                                    </div> <br />
                                                                                    <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                                                                            <div style="text-align:left">
                                                                                                 <asp:Label runat="server" Text="From" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                 <asp:Label runat="server" Text="To" style="margin-left:200px;font-size:12px"></asp:Label> <br />                                                                                           
                                                                                            </div>
                                                                                            <div >
                                                                                                <asp:TextBox ID="onewayFrom" runat="server"  Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>
                                                                                                    <asp:TextBox ID="onewayTo" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px; " Enabled="false"></asp:TextBox>
                                                                                                    <asp:TextBox ID="onewayDate" runat="server" Width="150px" Style="margin-left:20px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false"></asp:TextBox>

                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                                                                         <div style="text-align:left">
                                                                                                 <asp:Label runat="server" Text="From:" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                 <asp:Label runat="server" Text="To:" style="margin-left:200px;font-size:12px"></asp:Label> 
                                                                                                 <asp:Label runat="server" Text="Departure:" style="margin-left:200px;font-size:12px"></asp:Label>
                                                                                                 <asp:Label runat="server" Text="Return:" style="margin-left:100px;font-size:12px"></asp:Label>

                                                                                             <br />                                                                                           
                                                                                            </div>   
                                                                                            <asp:TextBox ID="round1From" runat="server"  Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                            <asp:TextBox ID="round1To" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                            <asp:TextBox ID="round2departure" runat="server"  Width="150px" Style="margin-left:20px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                            <asp:TextBox ID="round2return" runat="server" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                                                                            <!-- Multiple destinations flight input fields -->
                                                                                         <div style="text-align:left">
                                                                                                 <asp:Label runat="server" Text="From:" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                 <asp:Label runat="server" Text="To:" style="margin-left:200px;font-size:12px"></asp:Label> 
                                                                                                 <asp:Label runat="server" Text="Departure:" style="margin-left:180px;font-size:12px"></asp:Label>
                                                                                             <br />                                                                                           
                                                                                            </div>                                                                                           
        
                                                                                                <div id="destination1">
                                                                                                    <!--FIRST DESTINATION-->
                                                                                                    <asp:TextBox ID="TextBox7" runat="server"  Width="200px" Enabled="false" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>
                                                                                                    <asp:TextBox ID="TextBox8" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="TextBox12"  runat="server"  Width="150px" Enabled="false" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                 </div>   <br />
                                                                                                <div id="destination2">
                                                                                                    <!--SECOND DESTINATION-->
                                                                                                    <asp:TextBox ID="TextBox9" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"  ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                    <asp:TextBox ID="TextBox10" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="TextBox14" runat="server" Enabled="false" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                </div>                                                   
                                                                                            </div>
                                                                                        <div class="card-block" style="display:none" id="additionalFields" runat="server">
                                                                                                 <div id="destination3"> 
                                                                                                     <!--THIRD DESTINATION-->
                                                                                                    <asp:TextBox ID="TextBox15" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                    <asp:TextBox ID="TextBox17" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="TextBox18" runat="server" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                                    </div> <br />
                                                                                                 <div id="destination4" style="display:none" runat="server">
                                                                                                                <!--FOURTH DESTINATION-->
                                                                                                                <asp:TextBox ID="TextBox27" Enabled="false" runat="server" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"  ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                                <asp:TextBox ID="TextBox29" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                <asp:TextBox ID="TextBox30" Enabled="false" Width="150px"  runat="server" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                    </div>         <br />                                    
                                                                                                 <div id="destination5" style="display:none" runat="server">
                                                                                                            <!--FIFTH DESTINATION-->
                                                                                                            <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                   <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                     <asp:TextBox ID="TextBox21" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                            <asp:TextBox ID="TextBox22" runat="server" Enabled="false" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                    </div>   

                                                                                        </div>  <br /><br />                                                                                </div>

                                                                                </center>

                                                                                  <!--END OF FLIGHT INFORMATION-->
                                                                                <div class="card-block"  style="margin-left:250px">
                                                                                     <asp:Label ID="Label63" runat="server" Text="Remarks:"></asp:Label> <br />
                                                                                                <asp:TextBox ID="employeeRemarks" runat="server" style="width:600px ;margin-left:160px;text-align:center;border-radius:10px;color:black;background-color:transparent;font-size:18px" CssClass="textboxes"  Height="91px" Enabled="false"></asp:TextBox> 
                                                                                </div>


                                                                            </div>
                                                                            <div class="tab-pane" id="managerApproval" role="tabpanel">
                                                                                <div class="card-block" id="uploadBlock" style="display:none" runat="server">
                                                                                    <iframe id="pdfViewer"  runat="server" style="width:100%; display:none; height:600px" frameborder="0"></iframe>

                                                                                </div>                                                                            
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                          </div>

                                                        </div>
                                                            <asp:Button runat="server" class="btn btn-primary" Text="Process Request" ID="processRequest" OnClick="processRequest_Click"/>
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

