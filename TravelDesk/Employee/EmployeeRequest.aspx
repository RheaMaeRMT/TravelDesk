<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="EmployeeRequest.aspx.cs" Inherits="TravelDesk.Employee.EmployeeRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                  <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div class="page-header">
                          <div class="page-block">
                              <div class="row align-items-center">
                                  <div class="col-md-8">
                                      <div class="page-header-title">
                                          <h5 class="m-b-10">REQUEST MANAGEMENT</h5>
                                      </div>
                                  </div>
                                  <div class="col-md-4">
                                      <ul class="breadcrumb-title">
                                          <li class="breadcrumb-item">
                                              <a href="index.html"> <i class="fa fa-home"></i> </a>
                                          </li>
                                          <li class="breadcrumb-item"><a href="Employee/EmployeeDashboard">Dashboard</a>
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
                                                    <div class="card-block">
                                                        <asp:Label ID="Label13" runat="server" Text="View Travel Requests"></asp:Label>
                                                    </div>
                                        <section class="login-block">
                                            <!-- Container-fluid starts -->
                                            <div class="container">
                                                <div class="row">
                                                    <div class="col-sm-6" >
                                                        <a href="InternationalRequest" class="btnk">
                                                        <!-- First Box -->
                                                        <form class="md-float-material form-material">
                                                            <div class="text-center">
                                                            </div>
                                                            <div class="auth-box card">
                                                                <div class="card-block" style="background-color:#09426a">
                                                                    <div class="row m-b-20">
                                                                        <div class="col-md-12 pcoded-micon" style="text-align:center"><br />
                                                                             <img src="/images/icons8-plane-50.png" style="width: 50px; padding-top: 5px;" alt="planeIcon.png">
                                                                            <h3 class="text-center" style="color:white">INTERNATIONAL TRAVEL</h3>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </form>
                                                        </a>
                                                        <!-- end of first form -->
                                                    </div>

                                                    <!-- Second Box -->
                                                    <div class="col-sm-6"  style="margin-left: -30px; margin-right: -10px;">
                                                         <a href="DomesticRequest" class="btnk">
                                                            <form class="md-float-material form-material">
                                                            <div class="text-center">
                        
                                                            </div>
                                                            <div class="auth-box card">
                                                                <div class="card-block" style="background-color:#09426a">
                                                                    <div class="row m-b-20">
                                                                        <div class="col-md-12" style="text-align:center"><br />
                                                                             <img src="/images/icons8-plane-50.png" style="width: 50px; padding-top: 5px;" alt="planeIcon.png">
                                                                            <h3 class="text-center" style="color:white">DOMESTIC TRAVEL</h3>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </form>
                                                         </a>
                                                        <!-- end of second form -->
                                                    </div>

                                                    <!-- end of col-sm-12 -->

                                                </div>
                                                <!-- end of row -->
                                            </div>
                                            <!-- end of container-fluid -->
                                        </section>
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
