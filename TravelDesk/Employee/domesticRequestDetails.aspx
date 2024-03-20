<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="domesticRequestDetails.aspx.cs" Inherits="TravelDesk.Employee.domesticRequestDetails" %>

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
                                          <h5 class="m-b-10"> REQUEST DETAILS - DOMESTIC</h5>
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
                                         <div class="page-body">

                                                <div class="card" style="color:black;">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Domestic Travel Request Form</h5>
                                                    </div>
                                                        <div>
                                                            <div class="card-block">
                                                                 <asp:Label ID="Label15" runat="server" Text="Request Status: " style="font-size:16px" ></asp:Label>
                                                                <asp:Label runat="server" ID="statusRequest" ForeColor="Red" style="font-size:18px"></asp:Label>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label>
                                                                <asp:TextBox ID="employeeLocation" runat="server" Width="345px" CssClass="auto-style6"></asp:TextBox>
                                                            </div>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:80px" Width="345px"></asp:TextBox> 
                                                                <asp:Label ID="Label7" runat="server" Text="Employee Name" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeName" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label3" runat="server" Text="Designation"></asp:Label>
                                                                <asp:TextBox ID="employeeDesignation" runat="server"  CssClass="auto-style4" Width="341px"></asp:TextBox>

                                                                <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:150px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"  CssClass="auto-style5"  Width="260px"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label4" runat="server" Text="VOIP Ext."></asp:Label>
                                                                <asp:TextBox ID="employeeVoip" runat="server"  CssClass="auto-style1" Width="343px"></asp:TextBox>
                                                                <asp:Label ID="Label9" runat="server" Text="Mobile Number" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server"  Width="260px" CssClass="auto-style7"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label5" runat="server" Text="Project Code"></asp:Label>
                                                                <asp:TextBox ID="employeeProjCode" runat="server" CssClass="auto-style3" Width="341px"></asp:TextBox>

                                                            </div>
                                                            <!--TRAVEL DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label6" runat="server" Text="Home Facility" ></asp:Label>
                                                                <asp:TextBox ID="employeeFacility" runat="server" Width="343px" CssClass="auto-style11"></asp:TextBox> 

                                                                <asp:Label ID="Label10" runat="server" Text="Destination" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeDestination" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label11" runat="server" Text="Date of Departure"></asp:Label>
                                                       
                                                                <asp:TextBox ID="employeeDeparture" TextMode="Date" runat="server"  CssClass="m-l-50" Width="342px"></asp:TextBox>

                                                                <asp:Label ID="Label12" runat="server" Text="Date of Return"  style="padding-left:150px"></asp:Label>
                                                                <asp:TextBox ID="employeeReturn" TextMode="Date" runat="server"  CssClass="auto-style9"  Width="260px"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                <asp:DropDownList ID="employeePurpose" runat="server" CssClass="m-l-50" Width="343px">
                                                                    <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True"/>
                                                                    <asp:ListItem Text="Client Meeting" Value="Client Meeting" />
                                                                    <asp:ListItem Text="Business Summit" Value="Business Summit" />
                                                                    <asp:ListItem Text="Seminars" Value="Seminars" />
                                                                    <asp:ListItem Text="Facility Visit" Value="Facility Visit" />
    
                                                                </asp:DropDownList>

                                                                <asp:Label ID="Label14" runat="server" Text="Others" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeOthers" runat="server"  Width="260px" CssClass="auto-style10"></asp:TextBox>
                                                            </div>

                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label16" runat="server" Text="Manager Approval"></asp:Label>
                                                                <asp:DropDownList ID="employeeApproval" runat="server" CssClass="m-l-50" Width="343px" onchange="checkSelection()">
                                                                    <asp:ListItem Text="YES" Value="1"/>
                                                                    <asp:ListItem Text="NO" Value="0"  />
                                                                </asp:DropDownList>

                                                                <!-- manager name should auto-populate based on the manager assigned of the employees department -->
                                                                <asp:Label ID="Label17" runat="server" Text="Manager Name" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeManager" runat="server"  Width="236px" CssClass="auto-style13"></asp:TextBox>

                                                            </div>

                                                            <div class="card-block">
                                                                 <asp:Label ID="Label19" runat="server" Text="Approval Proof"></asp:Label> <br />
                                                                 <asp:Image CssClass="img-fluid img-thumbnail" ID="productImage" runat="server" Visible="False" />
                                                            </div>
                                                                
                                                            <div class="card-block">
                                                                 <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                                <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="auto-style10" TextMode="MultiLine" Height="91px"></asp:TextBox> 
                                                                
                                                            </div>

                                                            <div class="card-block">
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Modify" ID="updateRequest" OnClick="updateRequest_Click"/>

                                                            </div>
                                                    </div>
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
</asp:Content>
