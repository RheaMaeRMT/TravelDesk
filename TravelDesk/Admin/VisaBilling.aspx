<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisaBilling.aspx.cs" Inherits="TravelDesk.VisaBilling" %>
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
                                                                        </ul>
                                                                        <!-- Tab panes -->
                                                                        <div class="tab-content card-block">
                                                                            <div class="tab-pane active" id="billingInfo" role="tabpanel">
                                                                                    <%--  BILLING INFORMATION --%>
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block">
                                                                                                    <asp:Label ID="Label16" runat="server" Text="Visa Fee:" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="txtVisaFee" runat="server" Width="300px" Text="₱" Style="margin-left: 40px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                 </div> 
                                                                                             </div>
                                                                                       </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                          </div>
                                                    

                                                    <br />

                                                                                    
                                           </div>
                                                                <!--MODAL FOR REQUEST DETAILS -->
                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit" ID="submitVisaFee" OnClick="submitVisaFee_Click"/>
                                    
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

