<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="billingInformation.aspx.cs" Inherits="TravelDesk.Admin.billingInformation" %>
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
                            <div class="main-body" >
                                <div class="page-wrapper" >
                                    <!-- Page-body start -->
                                         <div class="page-body" style="color:black;font-size:16px;">
                                                <div class="card" style="color:black;background-color:white">
                                                    <div class="card-header" style="background-color:#09426a">
                                                                <asp:Label runat="server" style="color:white; font-size:16px;margin-left:10px;" CssClass="h5">Billing Process</asp:Label>
                                                    </div> 
                                                            <div class="card-block tab-icon">
                                                                    <div class="col-lg-12 ">
<%--                                                                        <div class="sub-title">Tab With Icon</div>--%>
                                                                        <!-- Nav tabs -->
                                                                        <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                         <li class="nav-item">
                                                                                <a class="nav-link active" data-toggle="tab" href="#billingInfo" role="tab"><i class="icofont icofont-ui-message"></i>Billing Information</a>
                                                                                <div class="slide"></div>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#employeeInfo" role="tab"><i class="icofont icofont-home"></i>Employee Information</a>
                                                                                <div class="slide"></div>
                                                                            </li> 
                                                                        </ul>
                                                                        <!-- Tab panes -->
                                                                        <div class="tab-content card-block">
                                                                            <div class="tab-pane active" id="billingInfo" role="tabpanel">
                                                                                    <%--  BILLING INFORMATION --%>
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
                                                                            </div>

                                                                            <div class="tab-pane" id="employeeInfo" role="tabpanel">   
                                                                                 <!--EMPLOYEE DETAILS-->
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
                                                                                
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                          </div>
                                                    

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
