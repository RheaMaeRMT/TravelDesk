<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TravelDesk.AdminDashboard" %>
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
                                          <h5 class="m-b-10">DASHBOARD</h5>
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
                                        <div class="row" style="color:black">
                                            <!-- task, page, download counter  start -->
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Approved" OnClick="approved_Click" CssClass="text-c-purple h4" BorderStyle="None" BackColor="Transparent"/> <br />
                                                                <a class="m-b-0">Approved Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-check-circle f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Processing" OnClick="approved_Click" CssClass="text-c-purple h4" BorderStyle="None" BackColor="Transparent"/> <br />
<%--                                                                <asp:Label ID="processing" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                <a class=" m-b-0">Processing Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-spinner f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Arranged" OnClick="approved_Click" CssClass="text-c-purple h4" BorderStyle="None" BackColor="Transparent"/> <br />
<%--                                                                <asp:Label ID="completed" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                <a class=" m-b-0">Arranged Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-check-square f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Completed" OnClick="approved_Click" CssClass="text-c-purple h4" BorderStyle="None" BackColor="Transparent"/> <br />
<%--                                                                <asp:Label ID="cancelled" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                <a class="m-b-0">Completed Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-ban f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
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
