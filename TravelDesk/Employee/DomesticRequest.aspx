<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="DomesticRequest.aspx.cs" Inherits="TravelDesk.Employee.DomesticRequest" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
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
                                                    <div class="card-block">
                                                        <asp:Label ID="Label1" runat="server" Text="Location"></asp:Label>
                                                        <asp:TextBox ID="IntLocationtxtbx" runat="server" Width="345px" CssClass="auto-style6"></asp:TextBox>
                                                    </div>
                                                    <!--EMPLOYEE DETAILS-->
                                                    <div class="card-block">
                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                        <asp:TextBox ID="Int" runat="server" style="margin-left:80px" Width="345px"></asp:TextBox> 
                                                        <asp:Label ID="Label7" runat="server" Text="Employee Name" style="padding-left:150px" ></asp:Label>
                                                        <asp:TextBox ID="TextBox1" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label3" runat="server" Text="Designation"></asp:Label>
                                                        <asp:TextBox ID="TextBox2" runat="server"  CssClass="auto-style4" Width="341px"></asp:TextBox>
                                                        <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:150px"></asp:Label>
                                                        <asp:TextBox ID="TextBox6" runat="server"  CssClass="auto-style5"  Width="260px"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label4" runat="server" Text="VOIP Ext."></asp:Label>
                                                        <asp:TextBox ID="TextBox3" runat="server"  CssClass="auto-style1" Width="343px"></asp:TextBox>
                                                        <asp:Label ID="Label9" runat="server" Text="Mobile Number" style="padding-left:150px" ></asp:Label>
                                                        <asp:TextBox ID="TextBox7" runat="server"  Width="260px" CssClass="auto-style7"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label5" runat="server" Text="Project Code"></asp:Label>
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="auto-style3" Width="341px"></asp:TextBox>
                                                    </div>
                                                    <!--TRAVEL DETAILS-->
                                                    <div class="card-block">
                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label6" runat="server" Text="Home Facility" ></asp:Label>
                                                        <asp:TextBox ID="TextBox5" runat="server" Width="343px" CssClass="auto-style11"></asp:TextBox> 
                                                        <asp:Label ID="Label10" runat="server" Text="Destination" style="padding-left:150px" ></asp:Label>
                                                        <asp:TextBox ID="TextBox8" runat="server"  Width="260px" style="margin-left:80px"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label11" runat="server" Text="Date of Departure"></asp:Label>
                                                       
                                                        <asp:TextBox ID="TextBox9" TextMode="DateTimeLocal" runat="server"  CssClass="m-l-50" Width="342px"></asp:TextBox>
                                                        <asp:Label ID="Label12" runat="server" Text="Date of Return"  style="padding-left:150px"></asp:Label>
                                                        
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server"></asp:RadioButtonList>                          <asp:TextBox ID="TextBox10" TextMode="DateTimeLocal" runat="server"  CssClass="auto-style9"  Width="260px"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                        <asp:TextBox ID="TextBox11" runat="server"  CssClass="m-l-50" Width="343px"></asp:TextBox>
                                                        <asp:Label ID="Label14" runat="server" Text="Others" style="padding-left:150px" ></asp:Label>
                                                        <asp:TextBox ID="TextBox12" runat="server"  Width="260px" CssClass="auto-style10"></asp:TextBox>
                                                    </div>
                                                    <!--ATTACHMENT-->
                                                    <div class="card-block">
                                                        <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Approval</p>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="Label16" runat="server" Text="Manager Approval"></asp:Label>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="m-l-50" Width="343px" onchange="checkSelection()">
                                                            <asp:ListItem Text="" Value="3" Selected="True"/>
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
                                                                        Manager Approval <strong> auto approves  </strong>your travel request. Requests with no prior manager approval needs to be <strong> manually approved</strong> by the manager. <br /> <br />
                                                                        Do you wish to proceed?
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <asp:Button runat="server" class="btn btn-primary" style="background-color:red" data-dismiss="modal" Text="NO"/>
                                                                         <asp:Button runat="server" class="btn btn-secondary" style="background-color:green" data-dismiss="modal" Text="YES"/>
                                                                       
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <!--javascript for the dropdown: manager approval options-->
                                                        <script type="text/javascript">
                                                        function checkSelection() {
                                                            var ddl = document.getElementById('<%= DropDownList1.ClientID %>');
                                                            var selectedValue = ddl.options[ddl.selectedIndex].value;

                                                            if (selectedValue === "0") {
                                                                // Display the NO modal
                                                                $('#noModal').modal('show');
                                                            } else if (selectedValue == "1") {
                                                                //display YES modal
                                                                $('#yesModal').modal('show');
                                                                document.getElementById('uploadBlock').style.display = 'block';
                                                                document.getElementById('submitCard').style.display = 'block';

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
                                                        <asp:TextBox ID="TextBox14" runat="server"  Width="236px" CssClass="auto-style13"></asp:TextBox>
                                                    </div>
                                                    <div class="card-block" id="uploadBlock" style="display:none">
                                                        <asp:Label ID="Label15" runat="server" Text="Attachment"></asp:Label>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="auto-style12" Width="348px" />
                                                    </div>
                                                    <div class="card-block">
                                                         <asp:Label ID="Label18" runat="server" Text="Remarks"></asp:Label> <br />
                                                        <asp:TextBox ID="TextBox13" runat="server"  Width="896px" CssClass="auto-style10" TextMode="MultiLine" Height="91px"></asp:TextBox>                                                    
                                                    </div>
                                                    <div class="card-block" id="submitCard" style="display:none;margin-left:600px">
                                                        <asp:Button runat="server" class="btn btn-primary" ID="submitBtn" Text="SUBMIT" style="background-color:#09426a;color:white"/>

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
