<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="TravelDesk.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
              <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/dashboard.png" style="width: 250px;" alt="logo.png">

                                </div>

                      </div>
                      <!-- Page-header end -->
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                      <div class="card">
                                                                                    <div class="card-header">
                                                                                        <h5 style="color:#09426a">Travel Requests</h5>
                                                                                     </div>  
                                                                                  <div class="page-wrapper">
                                                                                    <!-- Page-body start -->
                                                                                    <div class="page-body">
                                                                                        <div class="row" style="color:white">
                                                                                            <!-- task, page, download counter  start -->
                                                                                            <div class="col-xl-3 col-md-6">
                                                                                                <div class="card"  style="background-color:#09426a">
                                                                                                    <div class="card-block">
                                                                                                        <div class="row align-items-center">
                                                                                                            <div class="col-8">
                                                                                                                <asp:Button runat="server" ID="New" OnClick="approved_Click" CssClass="h4" style="color:white" BorderStyle="None" BackColor="Transparent"/> <br />
                                                                                                                <a class="m-b-0">New Requests</a>
                                                                                                            </div>
                                                                                                            <div class="col-4 text-right">
                                                                                                                <i class="fa fa-check-circle f-28"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-xl-3 col-md-6">
                                                                                                <div class="card"  style="background-color:#09426a">
                                                                                                    <div class="card-block">
                                                                                                        <div class="row align-items-center">
                                                                                                            <div class="col-8">
                                                                                                                <asp:Button runat="server" ID="Inprogress" OnClick="approved_Click" CssClass=" h4" BorderStyle="None" BackColor="Transparent"/> <br />
                                                <%--                                                                <asp:Label ID="processing" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                                                                <a class=" m-b-0">In-progress Requests</a>
                                                                                                            </div>
                                                                                                            <div class="col-4 text-right">
                                                                                                                <i class="fa fa-spinner f-28"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-xl-3 col-md-6">
                                                                                                <div class="card"  style="background-color:#09426a">
                                                                                                    <div class="card-block">
                                                                                                        <div class="row align-items-center">
                                                                                                            <div class="col-8">
                                                                                                                <asp:Button runat="server" ID="RequirementsSent" OnClick="approved_Click" CssClass=" h4" BorderStyle="None" BackColor="Transparent"/> <br />
                                                <%--                                                                <asp:Label ID="completed" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                                                                <a class=" m-b-0">Travel Requirements Sent</a>
                                                                                                            </div>
                                                                                                            <div class="col-4 text-right">
                                                                                                                <i class="fa fa-check-square f-28"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-xl-3 col-md-6">
                                                                                                <div class="card"  style="background-color:#09426a">
                                                                                                    <div class="card-block">
                                                                                                        <div class="row align-items-center">
                                                                                                            <div class="col-8">
                                                                                                                <asp:Button runat="server" ID="Completed" OnClick="approved_Click" CssClass="h4" BorderStyle="None" BackColor="Transparent"/> <br />
                                                <%--                                                                <asp:Label ID="cancelled" runat="server" class="text-c-purple h4"></asp:Label> <br />--%>
                                                                                                                <a class="m-b-0">Completed Requests</a>
                                                                                                            </div>
                                                                                                            <div class="col-4 text-right">
                                                                                                                <i class=" ti-medall f-28"></i>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <!-- Page-body end -->
                                                                                </div>
                                  
                                                                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
