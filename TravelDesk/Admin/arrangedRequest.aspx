<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="arrangedRequest.aspx.cs" Inherits="TravelDesk.Admin.arrangedRequest" EnableEventValidation="false" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .textboxes {
            color: black;
            background-color: transparent;
            border-radius: 5px;
        }
        
    </style>
    <script src="https://cdn.emailjs.com/dist/email.min.js"></script>

<script type="text/javascript">

    function showModal() {
        $('#sendPDFModal').modal('show');
        return false; // Prevents the default behavior of the button click event
    }


</script>

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
                                                <div class="card" style="color:black;background-color:white" ID="arrangementBlock">
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Travel Arrangement</h5>
                                                    </div>      
                                                                                     
                                                    <div class="card-block">
<%--                                                         <asp:Button runat="server" class="btn btn-primary" Text="Open Request" ID="openRequest" OnClientClick="return showModal();" /> --%>
                                                         <asp:Button runat="server" class="btn btn-primary" Text="Export as PDF" ID="exportasPdf" OnClick="exportasPdf_Click"/>
                                                         <asp:Button runat="server" class="btn btn-primary" Text="Send" ID="sendtoEmail" OnClientClick="showModal(); return false" />

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
                                                            
                                                            <!--ARRANGEMENTS -->
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Accomodations</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label5" runat="server" Text="Accomodations"></asp:Label>
                                                                <asp:TextBox ID="accomodations" runat="server"   Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotelAccomodations" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label1" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotel" runat="server" Width="300px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label4" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress" runat="server" Width="300px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label67" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact" runat="server" Width="300px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label9" runat="server" Text="Hotel Duration" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label10" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="durationFrom" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                <asp:Label ID="Label12" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="durationTo" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="careofEmployee" runat="server">
                                                                <asp:Label ID="Label17" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="employeeHotel" runat="server" Width="300px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Flight Details</p>

                                                            </div>
                                                             <div class="card-block">
                                                                <asp:Label ID="Label16" runat="server" Text="Airline" Style="margin-left: 20px;"></asp:Label>
                                                                <asp:TextBox ID="bookedairline" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> <hr /> <br /> 
                                                             </div> 
                                                                                        <!-- Row start -->
                                                                                        <div class="row">
                                                                                            <div class="col-lg-16 col-xl-8">
                                                                                                 <asp:Label ID="Label13" runat="server" Text="Flight Schedule" Style="margin-left: 30px;"></asp:Label>
                                                                                                <div class="card-block">
                                                                                                          <div class="card-block tab-icon">
                                                                                                                <div class="col-lg-12 ">
                                            <%--                                                                        <div class="sub-title">Tab With Icon</div>--%>
                                                                                                                    <!-- Nav tabs -->
                                                                                                                    <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                                                                     <li class="nav-item">
                                                                                                                            <a class="nav-link active" data-toggle="tab" href="#flightDetails" role="tab"><i class="icofont icofont-ui-message"></i>Flight Route</a>
                                                                                                                            <div class="slide"></div>
                                                                                                                        </li>
                                                                                                                        <li class="nav-item">
                                                                                                                            <a class="nav-link" data-toggle="tab" href="#transfersDetails" role="tab"><i class="icofont icofont-home"></i>Car/Airport Transfers</a>
                                                                                                                            <div class="slide"></div>
                                                                                                                        </li>

   
                                                                            
                                                                                                                    </ul>
                                                                                                                    <!-- Tab panes -->
                                                                                                                    <div class="tab-content card-block">
                                                                                                                        <div class="tab-pane active" id="flightDetails" role="tabpanel">
                                                                                                    <asp:Label ID="Label19" runat="server" Text="Travel Route" Style="margin-left: 20px;"></asp:Label><br />
                                                                                                                                <asp:Label ID="TextBox1" runat="server" Text="Flight #" Width="100px" Style="margin-left: 90px;font-size:14px"></asp:Label>
                                                                                                                                <asp:Label ID="TextBox2" runat="server" Text="Date of Departure" Width="120px" Style="margin-left: 20px;font-size:14px" TextMode="Date" CssClass="textboxes"></asp:Label> 
                                                                                                                                <asp:Label ID="TextBox3" runat="server" Text="From" Width="100px" Style="margin-left: 80px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="TextBox4" runat="server" Text="To" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="Label20" runat="server" Text="ETD" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="Label21" runat="server" Text="ETA" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <br />
                                                                                                                                <asp:TextBox ID="r1Flight" runat="server"  Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                <asp:TextBox ID="r1From" runat="server" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1To" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                
                                                                                                                            <div class="card-block" style="display:none;" id="additional2routeFields" runat="server">
                                                                                                                                     <asp:TextBox ID="r2Flight" runat="server"  Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>                                                                                                  
                                                                                                                                    <asp:TextBox ID="r2FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r2From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional3routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r3Flight" runat="server"  Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r3From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional4routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r4Flight" runat="server"  Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox>                                                                                                   
                                                                                                                                    <asp:TextBox ID="r4From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional5routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r5Flight" runat="server"  Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5FromDate" runat="server" Width="120px" Style="margin-left: 40px;" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r5From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>                                                                                               

                                                                                                                                </div>    
                                                                                                                            </div>
                                                                                                                        <div class="tab-pane" id="transfersDetails" role="tabpanel">
                                                                                                                                <asp:Label ID="Label15" runat="server" Text="Car/Airport Transfers" Style="margin-left: 30px;"></asp:Label>
                                                                                                                             <div class="card-block"  style="margin-left:10px" id="transfers1" runat="server">
                                                                                                                                  <asp:TextBox ID="transfer1" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                  <asp:TextBox ID="transfer1Date" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                            </div>
                                                                                                                             <div class="card-block" style="display:none;margin-left:10px" id="transfers2" runat="server">
                                                                                                                                  <asp:TextBox ID="transfer2" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                  <asp:TextBox ID="transfer2Date" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                            </div>
                                                                                                                             <div class="card-block" style="display:none;margin-left:10px" id="transfers3" runat="server">
                                                                                                                                  <asp:TextBox ID="transfer3" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                  <asp:TextBox ID="transfer3Date" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                            </div>
                                                                                                                             <div class="card-block" style="display:none;margin-left:10px" id="transfers4" runat="server">
                                                                                                                                  <asp:TextBox ID="transfer4" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                  <asp:TextBox ID="transfer4Date" runat="server" Width="150px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                            </div>
                                                                                                                             <div class="card-block" style="display:none;margin-left:10px" id="transfers5" runat="server">
                                                                                                                                  <asp:TextBox ID="transfer5" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                  <asp:TextBox ID="transfer5Date" runat="server" Width="150px" Style="margin-left: 40px;" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                            </div>

                                                                                                                        </div>

                                                                                                                    </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                      </div>

                                                                                            </div>
                                                                                        </div>
                                                                                        <!-- Row end --> 
                                                                                                                                                   <div class="card-block">
                                                                                            <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Others</p>
                                                                                        </div>
                                                                                         <div class="row">
                                                                                             <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" style="margin-left:50px">
                                                                                                    <asp:Label ID="Label65" runat="server" Text="Travel Requirements"></asp:Label> <br />
                                                                                                    <asp:TextBox ID="requirements" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                 </div> 

                                                                                             </div>
                                                                                             <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" style="margin-left:50px">
                                                                                                    <asp:Label ID="Label66" runat="server" Text="Additional Notes"></asp:Label> <br />
                                                                                                <asp:TextBox ID="additionalNotes" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                                                                 </div> 

                                                                                              </div>
                                                                                         </div>
                                           </div>
                                                                <!--MODAL FOR REQUEST DETAILS -->
                                                                <div class="modal fade" id="sendPDFModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog modal-md" role="document" style="max-width: 500px;">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title"> Travel Arrangement </h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                <h4>  <br />  </h4>
                                                                                <center>
                                                                              <asp:Label ID="Label18" runat="server"> Travel Arrangement has been successfully exported and downloaded. </asp:Label> 
                                                                                <asp:Label ID="label" runat="server"> Do you wish to send a copy to </asp:Label> <br />
                                                                                    <asp:Label runat="server" ID="employeeEmail"> </asp:Label>

                                                                                    <br /> <br />

                                                                                    <asp:LinkButton runat="server" class="ti-share btn btn-primary btn-round" style="font-size:18px;" ID="sendFile" OnClick="sendFile_Click"> Send </asp:LinkButton>
                                                                                </center>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div> 

                                             <asp:Button runat="server" class="btn btn-primary" Text="Proceed to Billing" ID="confirmArrangement" OnClick="confirmArrangement_Click" />
                                 
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
