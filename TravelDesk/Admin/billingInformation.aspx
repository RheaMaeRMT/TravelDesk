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
                                                                <asp:Label runat="server" ID="billingLabel" style="color:white; font-size:16px;margin-left:10px;" CssClass="h5"></asp:Label>
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
<%--                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#employeeInfo" role="tab"><i class="icofont icofont-home"></i>Employee Information</a>
                                                                                <div class="slide"></div>
                                                                            </li> --%>
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

<%--                                                                            <div class="tab-pane" id="employeeInfo" role="tabpanel">   
                                                                                 <!--EMPLOYEE DETAILS-->
                                                                                            <div class="row" style="place-content:center">
                                                                                                <div class="col-md-4">
                                                                                                    <div class="card-block" style="text-align:center;">
                                                                                                        <i class="ti-user" style="font-size:20px"></i> <br />
                                                                                                        <asp:Label runat="server" ID="empFName" style="font-size:22px;"></asp:Label> <br />
                                                                                                         <asp:Label runat="server" style="font-size:15px"> Employee ID:</asp:Label>
                                                                                                        <asp:Label runat="server" ID="empID" CssClass="h6"></asp:Label> <br />
                                                                                                         <asp:Label runat="server" style="font-size:15px"> Level:</asp:Label>
                                                                                                        <asp:Label ID="empLevel" runat="server" CssClass="h6"></asp:Label>
                                                                                                    </div>

                                                                                                </div>
                                                                                            </div> <br />
                                                                                            <div class="row" style="place-content:center">
                                                                                                 <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card" style="background-color:#C7E9FE">
                                                                                                         <div class="card-header" style="background-color:#09426a">
                                                                                                             <h5 style="color:white">Contact Information</h5>
                                                                                                         </div>
                                                                                                         <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                             <i class="ti-email"></i>
                                                                                                             <asp:Label runat="server" style="" > Email:</asp:Label>
                                                                                                              <asp:Label ID="empEmail" class="h6" style="margin-left:20px" runat="server"></asp:Label> <br />
                                                                                                             <i class="ti-mobile"></i>
                                                                                                             <asp:Label runat="server" style="" > Mobile:</asp:Label>
                                                                                                             <asp:Label ID="empMobile" class="h6" style="margin-left:10px" runat="server"></asp:Label> <br />
                                                                                                             <i class="ti-calendar"></i>
                                                                                                             <asp:Label runat="server" style="" > Birthdate:</asp:Label>
                                                                                                             <asp:Label ID="empBdate" class="h6" runat="server"></asp:Label>
                                                                                                         </div>
                                                                                                     </div>

                                                                                                </div>
                                                                                                 <div class="col-xl-4 col-md-12">
                                                                                                     <div class="card" style="background-color:#C7E9FE">
                                                                                                         <div class="card-header" style="background-color:#09426a">
                                                                                                             <h5 style="color:white">Company Information</h5>
                                                                                                         </div>
                                                                                                         <div class="card-block" style="text-align:left;font-size:15px">
                                                                                                             <i class="ti-id-badge"></i>
                                                                                                             <asp:Label runat="server" style="" > Project Code:</asp:Label>
                                                                                                           <asp:Label runat="server" class="h6" style="margin-left:22px" ID="empCode"></asp:Label> <br />
                                                                                                             <i class="ti-home"></i>
                                                                                                             <asp:Label runat="server" style="" > Home Facility:</asp:Label>
                                                                                                           <asp:Label runat="server" class="h6" style="margin-left:20px" ID="empFacility"></asp:Label> <br />
                                                                                                             <i class="ti-archive"></i>
                                                                                                             <asp:Label runat="server" style="" > Department Unit:</asp:Label>
                                                                                                           <asp:Label runat="server" class="h6" ID="empDeptUnit" style="margin-left:5px"></asp:Label> <br />
                                                                                                         </div>
                                                                                                     </div>

                                                                                                </div>

                                                                                            </div> <br />  
                                                                                
                                                                            </div>--%>

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
