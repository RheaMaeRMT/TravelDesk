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
                                                                <asp:TextBox ID="locationtxtbx" runat="server" Width="345px" CssClass="auto-style6"></asp:TextBox>
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
                                                                <asp:TextBox ID="employeeDU" runat="server"  CssClass="auto-style4" Width="341px"></asp:TextBox>
                                                                <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:150px"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server"  CssClass="auto-style5"  Width="260px"></asp:TextBox>
                                                            </div>
                                                    </div>
                                                 </div>
                                                             <asp:Button runat="server" class="btn btn-primary" Text="Enroll" ID="enrollBtn" OnClick="enrollBtn_Click" />

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
