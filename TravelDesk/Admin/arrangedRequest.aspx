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
    function showConfirmModal() {
        $('#downloadConfirmation').modal('show');
        return false; // Prevents the default behavior of the button click event
    }


</script>

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
                                                <div class="card" style="color:black;background-color:white" ID="arrangementBlock" runat="server">

                                                    <div class="card-header" style="background-color:#09426a">
                                                         <asp:Label runat="server" ID="statusLabel" style="color:white; font-size:16px;margin-left:10px;" CssClass="h5"></asp:Label>

                                                        </div>



                                                    <div class="card-block" style="margin-left:auto">
                                                          <asp:LinkButton runat="server" ID="exportPDF" class="btn btn-primary" style="color:white;font-size:16px;border-radius:20px;width:200px;" OnClientClick="showConfirmModal(); return false" OnClick="exportPDF_Click"> <i class="ti-export" style="color:white"></i> Export PDF </asp:LinkButton>     
                                                          <asp:LinkButton runat="server" ID="sendEmailbtn" class="btn btn-primary" style="display:none;color:white;font-size:16px;border-radius:20px;width:200px" OnClientClick="showModal(); return false"> <i class="ti-email" style="color:white"></i> send to email </asp:LinkButton>     
                                                    </div>
                                                    <center>
                                                            <div class="card-block">
                                                                <asp:Label runat="server" style="font-size:20px;color:black;padding-top:5px;padding-left:5px" CssClass="h5" ID="travellerName"></asp:Label>
                                                            </div>
                                                    </center>
                                                            <!--EMPLOYEE DETAILS-->
                                                            <div class="card-block">
                                                                <p style="font-size:18px;color:white;background-color:#09426a;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label7" runat="server" Text="Traveler Name"></asp:Label>
                                                                <asp:TextBox ID="employeeName" runat="server" Width="300px" Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                <asp:Label ID="Label2" runat="server" Text="Employee ID"  style="margin-left:40px;"></asp:Label>
                                                                <asp:TextBox ID="employeeID" runat="server" style="margin-left:40px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                                                                                    <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:60px"></asp:Label>
                                                                <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 40px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px"></asp:TextBox>
       
                                                                </div>
                                                            <div class="card-block">

                                                                <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                                <asp:TextBox  ID="homeFacility" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                           
                                                                <asp:Label ID="Label3" runat="server" Text="Mobile Number" Style="padding-left: 40px"></asp:Label>
                                                                <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                </div>
                                                            
                                                            <!--ARRANGEMENTS -->
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #09426a; padding-top: 5px; padding-left: 5px">Accomodations</p>
                                                            </div>
                                                            <div class="card-block" id="accomoDiv" runat="server">
                                                                <asp:Label ID="Label5" runat="server" Text="Accomodations"></asp:Label>
                                                                <asp:TextBox ID="accomodations" runat="server"   Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotelAccomodations" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label1" runat="server" Text="Hotel Name" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="hotel" runat="server" Width="200px" Style="margin-left: 30px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label67" runat="server" Text="Contact Number" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact" runat="server" Width="200px" Style="margin-left: 20px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                 <br /> <br />
                                                                <asp:Label ID="Label4" runat="server" Text="Address" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress" runat="server" Width="200px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label9" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="durationFrom" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel2Acc" runat="server">
                                                                <asp:Label ID="Label10" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label12" runat="server" Text="Hotel Name" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="hotel2" runat="server" Width="200px"  Style="margin-left: 30px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label14" runat="server" Text="Contact Number" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact2" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                 <br /> <br />
                                                                <asp:Label ID="Label22" runat="server" Text="Address" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress2" runat="server" Width="200px"  Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label23" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="durationFrom2" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel3Acc" runat="server">
                                                                <asp:Label ID="Label24" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label25" runat="server" Text="Hotel Name" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="hotel3" runat="server" Width="200px"  Style="margin-left: 30px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label26" runat="server" Text="Contact Number" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact3" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                 <br /> <br />
                                                                <asp:Label ID="Label27" runat="server" Text="Address" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress3" runat="server" Width="200px"  Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label28" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="durationFrom3" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel4Acc" runat="server">
                                                                <asp:Label ID="Label29" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label30" runat="server" Text="Hotel Name" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="hotel4" runat="server" Width="200px"  Style="margin-left: 30px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label31" runat="server" Text="Contact Number" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact4" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                 <br /> <br />
                                                                <asp:Label ID="Label32" runat="server" Text="Address" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress4" runat="server" Width="200px"  Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label33" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="durationFrom4" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel5Acc" runat="server">
                                                                <asp:Label ID="Label34" runat="server" Text="Hotel Accomodations"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label35" runat="server" Text="Hotel Name" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="hotel5" runat="server" Width="200px"  Style="margin-left: 30px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label36" runat="server" Text="Contact Number" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelContact5" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                 <br /> <br />
                                                                <asp:Label ID="Label37" runat="server" Text="Address" Style="margin-left: 30px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress5" runat="server" Width="200px"  Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="Label38" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="durationFrom5" runat="server" Width="200px"  Style="margin-left: 20px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                            </div>


                                                            <div class="card-block" style="display:none; margin-left:50px;" id="careofEmployee" runat="server">
                                                                <asp:Label ID="Label17" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="employeeHotel" runat="server" Width="300px" Style="margin-left: 50px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                            </div>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #09426a; padding-top: 5px; padding-left: 5px">Flight Information</p>
                                                                                                                         <div class="card-block">
                                                                                                                            <asp:Label ID="Label16" runat="server" Text="Airline" Style="margin-left: 20px;"></asp:Label>
                                                                                                                            <asp:TextBox ID="bookedairline" runat="server" Width="300px" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                         </div> 
                                                                                                                               <asp:Label ID="Label19" runat="server" Text="Itinerary" Font-Bold="true" Style="margin-left: 40px;" ></asp:Label><br />
                                                                                                                                <div class="card-block" id="additional1routeFields" runat="server">
                                                                                                                                <asp:Label ID="TextBox1" runat="server" Text="Flight #" Width="100px" Style="margin-left:40px;font-size:14px"></asp:Label>
                                                                                                                                <asp:Label ID="TextBox2" runat="server" Text="Date of Departure" Width="120px" Style="margin-left: 40px;font-size:14px" TextMode="Date" CssClass="textboxes"></asp:Label> 
                                                                                                                                <asp:Label ID="TextBox3" runat="server" Text="From" Width="100px" Style="margin-left: 65px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="TextBox4" runat="server" Text="To" Width="100px" Style="margin-left: 25px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="Label20" runat="server" Text="ETD" Width="100px" Style="margin-left: 25px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <asp:Label ID="Label21" runat="server" Text="ETA" Width="100px" Style="margin-left: 25px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                                                <br />
                                                                                                                                <asp:TextBox ID="r1Flight" runat="server"  Width="100px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                <asp:TextBox ID="r1From" runat="server" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1To" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                <asp:TextBox ID="r1ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional2routeFields" runat="server">
                                                                                                                                     <asp:TextBox ID="r2Flight" runat="server"  Width="100px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>                                                                                                  
                                                                                                                                    <asp:TextBox ID="r2FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r2From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r2ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional3routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r3Flight" runat="server"  Width="100px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r3From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r3ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional4routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r4Flight" runat="server"  Width="100px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4FromDate" runat="server" Width="120px" Style="margin-left: 40px;border-radius: 5px"  CssClass="textboxes" Enabled="false"></asp:TextBox>                                                                                                   
                                                                                                                                    <asp:TextBox ID="r4From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r4ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                </div>
                                                                                                                                <div class="card-block" style="display:none;" id="additional5routeFields" runat="server">
                                                                                                                                    <asp:TextBox ID="r5Flight" runat="server"  Width="100px" Style="margin-left: 40px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5FromDate" runat="server" Width="120px" Style="margin-left: 40px;" CssClass="textboxes" Enabled="false"></asp:TextBox> 
                                                                                                                                    <asp:TextBox ID="r5From" runat="server" PlaceHolder="From" Width="100px" Style="margin-left: 70px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5To" runat="server" PlaceHolder="To" Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5ETD" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>
                                                                                                                                    <asp:TextBox ID="r5ETA" runat="server"  Width="100px" Style="margin-left: 23px;border-radius: 5px" CssClass="textboxes" Enabled="false" ></asp:TextBox>                                                                                               

                                                                                                                                </div>    


                                                                                                                             <br /> <asp:Label ID="Label15" runat="server" Text="Car/Airport Transfers" Font-Bold="true" Style="margin-left: 40px;"></asp:Label>
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
                                                                                        <div class="card-block" id="remarksDiv" runat="server" style="display:none">
                                                                                            <p style="font-size: 18px; color: white; background-color: #09426a; padding-top: 5px; padding-left: 5px">Additional Information</p>
                                                                                            <div class="card-block">
                                                                    <asp:TextBox ID="remarks" runat="server" Width="1200px"  Height="100px"  TextMode="MultiLine" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="card-block">
                                                                                            <p style="font-size: 18px; color: white; background-color: #09426a; padding-top: 5px; padding-left: 5px">Others</p>
                                                                                        </div>
                                                                                         <div class="row">
                                                                                             <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" style="margin-left:50px">
                                                                                                    <asp:Label ID="Label65" runat="server" Text="Travel Requirements"></asp:Label> <br />
                                                                                                    <asp:TextBox ID="requirements" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                                 </div> 

                                                                                             </div>
                                                                                             <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" style="margin-left:50px;display:none" id="additionalDiv" runat="server">
                                                                                                    <asp:Label ID="Label66" runat="server" Text="Additional Notes"></asp:Label> <br />
                                                                                                <asp:TextBox ID="additionalNotes" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px;border-radius: 5px" CssClass="textboxes" Enabled="false"></asp:TextBox>

                                                                                                 </div> 

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
                                                                            <div class="modal-body" style="font-size:16px"> <br />
                                                                                <center>
                                                                              <asp:Label ID="Label18" runat="server"> Travel Arrangement has been successfully exported and downloaded. </asp:Label> 
                                                                                <asp:Label ID="label" runat="server"> Do you wish to send a copy to </asp:Label> <br />
                                                                                    <asp:Label runat="server" Font-Bold="true" ID="employeeEmail"> </asp:Label>

                                                                                    <br /> <br />
                                                                                         <asp:LinkButton runat="server" ID="LinkButton4"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px" OnClick="sendFile_Click"> <i class="ti-email" style="color:white"></i> SEND </asp:LinkButton>     
                                                                                </center>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                                                <div class="modal fade" id="downloadConfirmation" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog modal-md" role="document" style="max-width: 500px;">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title"> Travel Arrangement PDF </h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body" style="font-size:16px"> <br />
                                                                                <center>
                                                                              <asp:Label ID="Label39" runat="server"> Travel Arrangement has been exported to PDF</asp:Label> 
                                                                                <asp:Label ID="label40" runat="server"> </asp:Label> <br />
                                                                                    <asp:Label runat="server" Font-Bold="true" ID="Label41"> Click button to Download </asp:Label>

                                                                                    <br /> <br />
                                                                                         <asp:LinkButton runat="server" ID="confirmExport"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px" OnClick="confirmExport_Click"> <i class="ti-download" style="color:white"></i> Download </asp:LinkButton>     
                                                                                         <asp:LinkButton runat="server" ID="sendToEmail"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px"  OnClick="sendToEmail_Click"> <i class="ti-share" style="color:white"></i> Email Traveler </asp:LinkButton>     

                                                                                </center>

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
