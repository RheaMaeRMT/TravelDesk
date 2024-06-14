<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="visaRequestDetails.aspx.cs" Inherits="TravelDesk.Employee.visaRequestDetails" %>

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
                      <%--<!-- Page-header end -->--%>
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
                                                                <h5 style="color:white">Visa Request</h5>
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
                                                                                <asp:Label runat="server" style="font-size:16px">Current Status:
                                                                                </asp:Label>
                                                                                <asp:Label runat="server" ID="currentStatus" style="color:#4CAF50;font-size:18px;margin-left:10px"></asp:Label>
                                                                                <div class="tracker">
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="requestSubmittedCircle" data-hover-message="Visa Requests that has been submitted."><span>1</span></div>
                                                                                        <div class="text">Pending</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="processingCircle" data-hover-message="Visa Requests that has been accepted for processing."><span>2</span></div>
                                                                                        <div class="text">Processing</div>
                                                                                    </div>
                                                                                    <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="arrangedCircle" data-hover-message="Visa Requests that completed the process."><span>3</span></div>
                                                                                        <div class="text">Completed</div>
                                                                                    </div>
                                          <%--                                          <div class="line"></div>
                                                                                    <div class="stage">
                                                                                        <div class="circle" id="completedCircle" data-hover-message="Requests that has been granted a Visa for Travel"><span>4</span></div>
                                                                                        <div class="text">Granted</div>
                                                                                    </div>--%>
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
                                                                
                                                                                    <asp:Label ID="Label46" runat="server" Text="Email" Style="padding-left: 45px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeEmail" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false" ></asp:TextBox>



                                                                                </div>

                                                                                <!--TRAVEL DETAILS-->
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                                                </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                                    <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px" ></asp:TextBox>                                                              
                                                                                     <asp:Label ID="Label10" runat="server" Text="Destination" Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeDestination" runat="server"  Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        
                                                                                    <asp:Label ID="Label4" runat="server" Text="Estimated Travel Date"  Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="estTravelDate" runat="server"  Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        
                                                            
                                                                                </div>
                                                                                <div class="card-block">

                                                                                </div>
                                                                                <div class="card-block">

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

