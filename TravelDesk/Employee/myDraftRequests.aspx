<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="myDraftRequests.aspx.cs" Inherits="TravelDesk.Employee.myDraftRequests" %>
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
                                          <h5 class="m-b-10">SAVED DRAFTS</h5>
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
                                       <div class="page-body"><br />
                                           <section class="login-block">
                                            <!-- Container-fluid starts -->
                                            <div class="container">
                                                <div class="row">
                                                    <div class="col-sm-6" >
                                                        <a class="btnk">
                                                        <!-- First Box -->
                                                        <div class="md-float-material form-material">
                                                            <div class="text-center">
                                                            </div>
                                                            <div class="auth-box card">
                                                                <div class="card-block" style="background-color:#09426a">
                                                                    <div class="row m-b-20">
                                                                        <div class="col-md-12 pcoded-micon" style="text-align:center"><br />
                                                                             <img src="/images/icons8-plane-50.png" style="width: 50px; padding-top: 5px;" alt="planeIcon.png"> <br />
                                                                            <asp:LinkButton runat="server" ID="visaRequests" Text ="VISA REQUEST DRAFTS" CssClass="text-center" style="color:white;font-size:30px" OnClick="visaRequests_Click"></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </a>
                                                        <!-- end of first form -->
                                                    </div>

                                                    <!-- Second Box -->
                                                    <div class="col-sm-6"  style="margin-left: -30px; margin-right: -10px;">
                                                         <a class="btnk">
                                                            <div class="md-float-material form-material">
                                                            <div class="text-center">
                        
                                                            </div>
                                                            <div class="auth-box card">
                                                                <div class="card-block" style="background-color:#09426a">
                                                                    <div class="row m-b-20">
                                                                        <div class="col-md-12" style="text-align:center"><br />
                                                                             <img src="/images/icons8-plane-50.png" style="width: 50px; padding-top: 5px;" alt="planeIcon.png"> <br />
                                                                            <asp:LinkButton runat="server" ID="traveldraftRequests" Text ="TRAVEL REQUEST DRAFTS" CssClass="text-center" style="color:white;font-size:30px" OnClick="travelRequests_Click"></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         </a>
                                                        <!-- end of second form -->
                                                    </div>

                                                    <!-- end of col-sm-12 -->

                                                </div>
                                                <!-- end of row -->
                                            </div>
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

