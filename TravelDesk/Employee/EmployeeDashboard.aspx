<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" Inherits="TravelDesk.Employee.EmployeeDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
                .a:hover::after {
            content: attr(data-hover-message); /* Display the hover message */
            position: absolute;
             bottom: -50px; /* Position the hover message above the circle */
            left: calc(20% + 5px); /* Position the hover message to the right of the circle */
            padding: 5px;
            width: 200px;
            background-color:lightgrey; /* Background color of the hover message */
            color: black; /* Text color of the hover message */
            font-size: 12px;
            border-radius: 5px;
            text-align:center;
            z-index: 999; /* Ensure the hover message appears above other elements */
        }
    </style>
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
                                     <h5 style="color:#09426a"></h5>
                                 </div>  
                                 <div class="page-wrapper">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                        <div class="row" style="color:white">
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card" style="background-color:#09426a">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="New" OnClick="approved_Click" CssClass="h4"  style="color:white"  BorderStyle="None" BackColor="Transparent"/> <br />
                                                                <a class="m-b-0 a" data-hover-message="New Requests that has been submitted." >New Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-check-circle f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xl-3 col-md-6">
                                                <div class="card" style="background-color:#09426a">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Approved" OnClick="approved_Click" CssClass="h4"  style="color:white"  BorderStyle="None" BackColor="Transparent"/> <br />
                                                                <a class="m-b-0 a" data-hover-message="Requests that has been accepted for processing." >Processing Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-check-circle f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card" style="background-color:#09426a">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Processing" OnClick="approved_Click" CssClass=" h4"  style="color:white"  BorderStyle="None" BackColor="Transparent"/> <br />
                                                                <a class=" m-b-0 a" data-hover-message="Requests that completed the request processing.">Completed Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-spinner f-28"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xl-3 col-md-6">
                                                <div class="card" style="background-color:#09426a">
                                                    <div class="card-block">
                                                        <div class="row align-items-center">
                                                            <div class="col-8">
                                                                <asp:Button runat="server" ID="Arranged" OnClick="approved_Click" CssClass="h4"   style="color:white" BorderStyle="None" BackColor="Transparent"/> <br />
                                                                <a class=" m-b-0 a" data-hover-message="Requests that completed the process and is closed." >Closed Requests</a>
                                                            </div>
                                                            <div class="col-4 text-right">
                                                                <i class="fa fa-check-square f-28"></i>
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

