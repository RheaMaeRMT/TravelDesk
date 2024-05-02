<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="EmployeeRequest.aspx.cs" Inherits="TravelDesk.Employee.EmployeeRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                                    <div class="page-body">
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
                                                                            <asp:LinkButton runat="server" ID="international" Text ="INTERNATIONAL TRAVEL" CssClass="text-center" style="color:white;font-size:30px" OnClick="international_Click"></asp:LinkButton>
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
                                                                            <asp:LinkButton runat="server" ID="domestic" Text ="DOMESTIC TRAVEL" CssClass="text-center" style="color:white;font-size:30px" OnClick="domestic_Click"></asp:LinkButton>
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
<%--                                                <center>
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
                                                                            <asp:LinkButton runat="server" ID="visaRequest" Text ="VISA APPLICATION" CssClass="text-center" style="color:white;font-size:30px" OnClick="visaRequest_Click"></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         </a>
                                                        <!-- end of second form -->
                                                    </div>

                                                </center>--%>

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
