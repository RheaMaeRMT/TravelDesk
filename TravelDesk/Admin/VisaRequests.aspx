<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisaRequests.aspx.cs" Inherits="TravelDesk.Admin.VisaRequests" %>

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
                                    <img src="/images/visaRequests.png" style="width: 250px;" alt="logo.png">

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
                                                                                <a class="nav-link" data-toggle="tab" href="#pdfFiles" role="tab"><i class="icofont icofont-ui-user "></i>Uploaded Files</a>
                                                                                <div class="slide"></div>
                                                                            </li>
   
                                                                            
                                                                        </ul>
                                                                        <!-- Tab panes -->
                                                                        <div class="tab-content card-block">
                                                                            <div class="tab-pane active" id="requestTracking" role="tabpanel">
                                                                                <asp:Label runat="server" style="font-size:16px">Current Status:
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="currentStatus" style="color:#4CAF50;font-size:18px;margin-left:10px"></asp:Label>
                                                                                <div class="tracker">
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="requestSubmittedCircle" data-hover-message="Requests that has been submitted and auto-approved."><span>1</span></div>
                                                                                        <div class="text">Pending</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="processingCircle" data-hover-message="Requests that has been accepted for travel arrangement processing."><span>2</span></div>
                                                                                        <div class="text">Processing</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="arrangedCircle" data-hover-message="Requests that completed the travel arrangement process."><span>3</span></div>
                                                                                        <div class="text">Completed</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="completedCircle" data-hover-message="Requests that completed the travel arrangement and billing process"><span>4</span></div>
                                                                                        <div class="text">Granted</div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="tab-pane" id="requestDetails" role="tabpanel">
                                                                                            <div class="row" style="place-content:center">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="card-block" style="text-align:center;">
                                                                                                        <i class="ti-user" style="font-size:20px"></i> <br />
                                                                                                        <asp:Label runat="server" ID="empFName" style="font-size:22px;"></asp:Label> <br />
                                                                                                             <asp:Label runat="server" style="" >Department Unit:</asp:Label>
                                                                                                           <asp:Label runat="server" class="h6" ID="empDeptUnit"></asp:Label>  <br />
                                                                                                         <asp:Label runat="server" style="font-size:15px"> Employee ID:</asp:Label>
                                                                                                        <asp:Label runat="server" ID="empID" class="h6"></asp:Label> <br />
                                                                                                         <asp:Label runat="server" style="font-size:15px"> Level:</asp:Label>
                                                                                                        <asp:Label ID="empLevel" runat="server"  class="h6"></asp:Label>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div> <br />
                                                                                            <div class="row" style="place-content:center">
                                                                                                 <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card">
                                                                                                         <div class="card-header">
                                                                                                             <h5>Contact Information</h5>
                                                                                                         </div>
                                                                                                         <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                             <i class="ti-email"></i>
                                                                                                             <asp:Label runat="server" style="" > Email:</asp:Label>
                                                                                                              <asp:Label ID="empEmail" class="h6" runat="server" style="margin-left:30px"></asp:Label> <br />
                                                                                                             <i class="ti-mobile"></i>
                                                                                                             <asp:Label runat="server" style="" > Mobile:</asp:Label>
                                                                                                             <asp:Label ID="empMobile" class="h6" runat="server"  style="margin-left:20px"></asp:Label> <br />
                                                                                                             <i class="ti-calendar"></i>
                                                                                                             <asp:Label runat="server" style="" > Birthdate:</asp:Label>
                                                                                                             <asp:Label ID="empBdate" class="h6" runat="server"  style="margin-left:5px"></asp:Label>
                                                                                                         </div>
                                                                                                     </div>

                                                                                                </div>
                                                                                                 <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card">
                                                                                                         <div class="card-header">
                                                                                                             <h5>Travel Information</h5>
                                                                                                         </div>
                                                                                                         <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                             <i class="ti-comment-alt"></i>
                                                                                                             <asp:Label runat="server" style="" > Purpose of Travel:</asp:Label>
                                                                                                              <asp:Label ID="employeePurpose" class="h6" runat="server"></asp:Label> <br />
                                                                                                             <i class="ti-map-alt"></i>
                                                                                                             <asp:Label runat="server" style="" > Destination:</asp:Label>
                                                                                                             <asp:Label ID="empDestination" class="h6" runat="server" style="margin-left:40px"></asp:Label> <br />
                                                                                                             <i class="ti-calendar"></i>
                                                                                                             <asp:Label runat="server" style="" > Est. Travel Date:</asp:Label>
                                                                                                             <asp:Label ID="EmpestTravelDate" class="h6" runat="server" style="margin-left:20px"></asp:Label>
                                                                                                         </div>
                                                                                                     </div>

                                                                                                </div>

                                                                                            </div> <br />                                                                                 
                                                                            </div>
                                                                            <div class="tab-pane" id="pdfFiles" role="tabpanel">
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
                                                            <asp:Button runat="server" class="btn btn-primary" Text="Process Request" ID="processRequest"/>
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

