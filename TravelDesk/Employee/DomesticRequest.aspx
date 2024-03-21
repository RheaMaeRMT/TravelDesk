<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="DomesticRequest.aspx.cs" Inherits="TravelDesk.Employee.DomesticRequest" %>

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
                                         <div class="page-body">
                                                <div class="card" style="color:black;">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Domestic Travel Request Form</h5>
                                                    </div>
                                                        <div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label>
                                                                <asp:TextBox ID="employeeLocation" runat="server" Width="345px" CssClass="auto-style6"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeLocation"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:80px" Width="345px"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeID"></asp:RequiredFieldValidator>
                                                                <asp:Label ID="Label7" runat="server" Text="Employee Name" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeName" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeName"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label3" runat="server" Text="Designation"></asp:Label>
                                                                <asp:TextBox ID="employeeDesignation" runat="server"  CssClass="auto-style4" Width="341px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeDesignation"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:150px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"  CssClass="auto-style5"  Width="260px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeLevel"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label4" runat="server" Text="VOIP Ext."></asp:Label>
                                                                <asp:TextBox ID="employeeVoip" runat="server"  CssClass="auto-style1" Width="343px"></asp:TextBox>
                                                                <asp:Label ID="Label9" runat="server" Text="Mobile Number" style="padding-left:160px" ></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server"  Width="260px" CssClass="auto-style7"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeePhone"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label5" runat="server" Text="Project Code"></asp:Label>
                                                                <asp:TextBox ID="employeeProjCode" runat="server" CssClass="auto-style3" Width="341px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeProjCode"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <!--TRAVEL DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label6" runat="server" Text="Home Facility" ></asp:Label>
                                                                <asp:TextBox ID="employeeFacility" runat="server" Width="343px" CssClass="auto-style11"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeFacility"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label10" runat="server" Text="Destination" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeDestination" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeDestination"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label11" runat="server" Text="Date of Departure"></asp:Label>
                                                       
                                                                <asp:TextBox ID="employeeDeparture" TextMode="Date" runat="server"  CssClass="m-l-50" Width="342px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeDeparture"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label12" runat="server" Text="Date of Return"  style="padding-left:150px"></asp:Label>
                                                                <asp:TextBox ID="employeeReturn" TextMode="Date" runat="server"  CssClass="auto-style9"  Width="260px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeReturn"></asp:RequiredFieldValidator>

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
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeePurpose"></asp:RequiredFieldValidator>

                                                                <asp:Label ID="Label14" runat="server" Text="Others" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeOthers" runat="server"  Width="260px" CssClass="auto-style10"></asp:TextBox>
                                                            </div>

                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label16" runat="server" Text="Manager Approval"></asp:Label>
                                                                <asp:DropDownList ID="employeeApproval" runat="server" CssClass="m-l-50" Width="343px" onchange="checkSelection()">
                                                                    <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True"/>
                                                                    <asp:ListItem Text="YES" Value="1" />
                                                                    <asp:ListItem Text="NO" Value="0" />
                                                                </asp:DropDownList>

                                                                <!--MODAL FOR THE NO OPTION -->
                                                                <div class="modal fade" id="noModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog" role="document">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title" id="noModalLabel">Manager Approval</h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                <!-- Your modal content goes here -->
                                                                                Manager Approval <strong> auto approves  </strong>your travel request. <br /> Requests with no prior manager approval needs to be <strong> manually approved</strong> by the manager. <br /> <br />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <!--javascript for the dropdown: manager approval options-->
                                                                <script type="text/javascript">
                                                                    function checkSelection() {
                                                                        var ddl = document.getElementById('<%= employeeApproval.ClientID %>');
                                                                        var selectedValue = ddl.options[ddl.selectedIndex].value;

                                                                        if (selectedValue === "0") {
                                                                            // Display the NO modal
                                                                            $('#noModal').modal('show');
                                                                            document.getElementById('uploadBlock').style.display = 'none';
                                                                        } else if (selectedValue == "1") {
                                                                            //display YES modal
                                                                            $('#yesModal').modal('show');
                                                                            document.getElementById('uploadBlock').style.display = 'block';
                                                                            document.getElementById('submitCard').style.display = 'block';
                                                                            document.getElementById('uploadbtn').style.display = 'block';

                                                                        }

                                                                    }
                                                                    

                                                                </script>                                                       
                                                        
                                                                <!--MODAL FOR THE YES OPTION -->
                                                                <div class="modal fade" id="yesModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog" role="document">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title" id="yesModalLabel">Manager Approval</h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                <!-- Your modal content goes here -->
                                                                                Please upload an Attachment:<strong>Proof of Manager Approval </strong> 
                                                                            </div>
                                                                            <div class="modal-footer">
                                                                                 <asp:Button runat="server" class="btn btn-secondary" style="background-color:green" data-dismiss="modal" Text="Proceed" OnClientClick="return false;"/>
                                                                       
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!-- manager name should auto-populate based on the manager assigned of the employees department -->
                                                                <asp:Label ID="Label17" runat="server" Text="Manager Name" style="padding-left:150px" ></asp:Label>
                                                                <asp:TextBox ID="employeeManager" runat="server"  Width="236px" CssClass="auto-style13"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeManager"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="card-block" id="uploadBlock" style="display:none">
                                                                <asp:Label ID="Label15" runat="server" Text="Approval Proof"></asp:Label>
                                                                <label ID="uploadStatus" runat="server" Text="Attachment"></label>
                                                                <asp:FileUpload ID="employeeUpload" type="file" runat="server" CssClass="auto-style12" Width="348px" />
                                                                <asp:Button class="form-control btn btn-primary btn-sm"  ID="Button1" runat="server" Text="Upload" OnClick="btnUpload_Click" Width="150px" style="margin-left:10px" CausesValidation="False" />

                                                            </div> <br />


                                                                <div class="col-md-5">
                                                                    <asp:Image CssClass="img-fluid img-thumbnail" ID="productImage" runat="server" Visible="False" />
                                                                </div>
                                                            
                                                            <div class="card-block">
                                                                 <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                                <asp:TextBox ID="employeeRemarks" runat="server"  Width="896px" CssClass="auto-style10" TextMode="MultiLine" Height="91px"></asp:TextBox> 
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" CssClass="required" ControlToValidate="employeeRemarks"></asp:RequiredFieldValidator>
                                                                
                                                            </div>
                                                    </div>
                                                 </div>
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit" ID="submitRequestbtn" OnClick="submitRequestbtn_Click"/>

<%--                                                 <asp:Button runat="server" class="btn btn-primary" ID="submitBtn" Text="SUBMIT" OnClick="submitBtn_Click"/>--%>

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
