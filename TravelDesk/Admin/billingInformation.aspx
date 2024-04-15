<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="billingInformation.aspx.cs" Inherits="TravelDesk.Admin.billingInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div class="page-header" style="background-color:#09426a">
                          <div class="page-block">
                              <div class="row align-items-center">
                                  <div class="col-md-8">
                                      <div class="page-header-title">
                                          <h5 class="m-b-10">TRAVEL ARRANGEMENT</h5>
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
                            <div class="main-body" >
                                <div class="page-wrapper" >
                                    <!-- Page-body start -->
                                         <div class="page-body" style="color:black;font-size:16px;">
                                                <div class="card" style="color:black;background-color:white">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Travel Arrangement Form</h5>
                                                    </div>      
                                                         <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label7" runat="server" Text="Traveller Name"></asp:Label>
                                                                <asp:TextBox ID="employeeName" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID"  style="margin-left:40px;"></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:60px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px"></asp:TextBox>
       
                                                                </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                                <asp:TextBox  ID="homeFacility" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                           
                                                                <asp:Label ID="Label3" runat="server" Text="Mobile Number" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                             </div>

<%--                                                       BILLING INFORMATION --%>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Billing Information</p>
                                                            </div>
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block">
                                                                                                    <asp:Label ID="Label16" runat="server" Text="Hotel Charges" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelCharges" runat="server" Width="300px" Text="₱" Style="margin-left: 40px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                 </div> 
                                                                                                 <div class="card-block">
                                                                                                    <asp:Label ID="Label1" runat="server" Text="Plane Fare" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="planeFare" runat="server" Width="300px" Text="₱" Style="margin-left: 60px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                 </div> 
                                                                                             </div>
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block">
                                                                                                    <asp:Label ID="Label4" runat="server" Text="Per Diem" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="perDiem" runat="server" Width="300px" Text="₱" Style="margin-left: 60px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                 </div> 
                                                                                                 <div class="card-block">
                                                                                                    <asp:Label ID="Label5" runat="server" Text="Transfers" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="transfersFee" runat="server" Width="300px" Text="₱" Style="margin-left: 60px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                 </div> 
                                                                                             </div>
                                                                                       </div>
                                                                                                <center>
                                                                                             <asp:Button runat="server" class="btn btn-primary" Text="Calculate" ID="calculate" CausesValidation="false" Width="100px" OnClick="calculate_Click" /> <br />
                                                                                                 <div class="card-block" style="display:none" runat="server" id="totalBlock"> <br />
                                                                                                    <asp:Label ID="Label6" runat="server" Text="Total Expenses:" style="margin-left:-120px"></asp:Label>
                                                                                                    <asp:TextBox ID="totalExpensesTxt" runat="server" Width="300px" Style="margin-left: 40px;color:black; font-size:16px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                 </div> 

                                                                                                </center>
                                                    <br />

                                                                                    
                                           </div>
                                                                <!--MODAL FOR REQUEST DETAILS -->
                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit" ID="submitArrangement" OnClick="submitArrangement_Click"/>
                                    
                                    <!-- Page-body end -->
                                         </div>
                                <div id="styleSelector"> </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
  
</asp:Content>
