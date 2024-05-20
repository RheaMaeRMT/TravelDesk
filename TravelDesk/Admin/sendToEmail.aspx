<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sendToEmail.aspx.cs" Inherits="TravelDesk.Admin.sendToEmail" %>
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
                                    <center>
                                         <div class="card col-xl-6 col-md-12" style="color:black;font-size:16px;">
                                                <div class="card-block" style="color:black;background-color:white" ID="arrangementBlock" runat="server">    
                                                    <div >
                                                        <div class="card-block">                                                                               
                                                                <asp:Label ID="Label18" runat="server"> Travel Arrangement has been successfully exported and downloaded. </asp:Label> 
                                                        </div>
                                                    </div>
                                                       <asp:LinkButton runat="server" ID="LinkButton1"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px"> <i class="ti-email" style="color:white"></i>  </asp:LinkButton>     
                                                       <asp:LinkButton runat="server" ID="LinkButton4"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px"> <i class="ti-email" style="color:white"></i> SEND </asp:LinkButton>     

                                                </div>

                                           </div>
                                    </center>

                                    <!-- Page-body end -->
                                </div>
                                <div id="styleSelector"> </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
