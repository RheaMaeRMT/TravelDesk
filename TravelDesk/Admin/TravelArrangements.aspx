<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelArrangements.aspx.cs" Inherits="TravelDesk.Admin.TravelArrangements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript">
        function showModal() {
            $('#requestModal').modal('show');
            return false; // Prevents the default behavior of the button click event
        }


        function accomodationOptions() {
            var accomodations = document.getElementById('<%= accomodations.ClientID %>');
            var selectedOption = accomodations ? accomodations.options[accomodations.selectedIndex].value : null;


            if (selectedOption === "Hotel Accomodation") {
                document.getElementById('<%= hotelAccomodations.ClientID %>').style.display = 'block';
                document.getElementById('<%= travellerAccomodation.ClientID%>').style.display = 'none';

            } else {
                document.getElementById('<%= hotelAccomodations.ClientID %>').style.display = 'none';
                document.getElementById('<%= travellerAccomodation.ClientID%>').style.display = 'block';

            }
        }
        function add2Hotel(button) {
            document.getElementById('<%=hotel2.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button5.ClientID%>').style.display = 'none';
        }
        function add3Hotel(button) {
            document.getElementById('<%=hotel3.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button6.ClientID%>').style.display = 'none';

        }
        function add4Hotel(button) {
            document.getElementById('<%=hotel4.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button7.ClientID%>').style.display = 'none';

        }
        function add5Hotel(button) {
            document.getElementById('<%=hotel5.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button8.ClientID%>').style.display = 'none';

        }

        function add2Route(button) {
            document.getElementById('<%= additional2routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add2nd.ClientID %>').style.display = 'none';

        }
        function add3Route(button) {
            document.getElementById('<%= additional3routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add3rd.ClientID %>').style.display = 'none';

        }
        function add4Route(button) {
            document.getElementById('<%= additional4routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add4th.ClientID %>').style.display = 'none';

        }
        function add5Route(button) {
            document.getElementById('<%= additional5routeFields.ClientID %>').style.display = 'block';
            document.getElementById('<%= add5th.ClientID %>').style.display = 'none';

        }
        function add2Transfer(button) {
            document.getElementById('<%= transfers2.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button1.ClientID %>').style.display = 'none';

        } function add3Transfer(button) {
            document.getElementById('<%= transfers3.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button2.ClientID %>').style.display = 'none';

        } function add4Transfer(button) {
            document.getElementById('<%= transfers4.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button3.ClientID %>').style.display = 'none';

        } function add5Transfer(button) {
            document.getElementById('<%= transfers5.ClientID %>').style.display = 'block';
            document.getElementById('<%= Button4.ClientID %>').style.display = 'none';

        }





    </script>
    <style>
        .checkbox-list {
    padding-bottom: 20px; /* Add space at the bottom of the checkbox list */
}

.checkbox-list label {    margin-bottom: 10px; /* Add space between checkbox items */
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
                                             <div  class="card" style="color:black;background-color:white"> 
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <asp:Label runat="server" ID="ArrangementLabel" style="color:white;font-size:16px" ></asp:Label>
                                                    </div> 
                                                                                     
                                                    <div class="card-block">
                                                        <asp:LinkButton runat="server" Text="Open Request Details" style="font-size:16px;color:dodgerblue" Font-Underline="true" ID="openRequestDetails" OnClientClick="return showModal();"></asp:LinkButton>

                                                    </div>
                                                            <!--ARRANGEMENTS -->
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Accomodations</p>
                                                            </div>
                                                            <div class="card-block">
                                                                <asp:Label ID="Label5" runat="server" Text="Accomodations"></asp:Label>
                                                                <asp:DropDownList ID="accomodations" runat="server"  Style="margin-left: 60px" Width="345px" onchange="accomodationOptions()" OnSelectedIndexChanged="accomodations_SelectedIndexChanged">
                                                                <asp:ListItem Text="-- Select Option --" Value="" Disabled="true" Selected="True" />
                                                                    <asp:ListItem Value="Hotel Accomodation" Text="Hotel Accomodation"> </asp:ListItem>
                                                                    <asp:ListItem Value="c/o Traveler" Text="c/o Traveler">  </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotelAccomodations" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Hotel Accomodations"></asp:Label>
                                                                <asp:LinkButton ID="getsavedHotels" runat="server" style="color:white;font-size:13px;margin-left:1150px;border-radius:5px" CssClass="btn btn-primary" OnClick="getsavedHotels_Click"> <i class="ti-bookmark-alt"></i> get Saved Hotels</asp:LinkButton>
                                                                <br /> <br />
                                                                <asp:Label ID="Label1" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotel" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label4" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelAddress" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox>
                                                                <asp:Label ID="Label67" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="hotelPhone" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label9" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label10" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="durationFrom" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label12" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="durationTo" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Button runat="server" ID="Button5" class="btn btn-primary" Text="+" OnClientClick="add2Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel2" runat="server"> <hr />
                                                                <asp:Label ID="Label17" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotelname2" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label18" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="address2" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox>
                                                                <asp:Label ID="Label19" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="phone2" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label20" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label21" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="from2" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label22" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="to2" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Button runat="server" ID="Button6" class="btn btn-primary" Text="+" OnClientClick="add3Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel3" runat="server"> <hr />
                                                                <asp:Label ID="Label23" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotelname3" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label24" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="address3" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox>
                                                                <asp:Label ID="Label25" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="phone3" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label26" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label27" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="from3" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label28" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="to3" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Button runat="server" ID="Button7" class="btn btn-primary" Text="+" OnClientClick="add4Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel4" runat="server"> <hr />
                                                                <asp:Label ID="Label29" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotelname4" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label31" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="address4" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox>
                                                                <asp:Label ID="Label32" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="phone4" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label33" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label34" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="from4" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label35" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="to4" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Button runat="server" ID="Button8" class="btn btn-primary" Text="+" OnClientClick="add5Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                            </div>
                                                            <div class="card-block" style="display:none; margin-left:50px;" id="hotel5" runat="server"> <hr />
                                                                <asp:Label ID="Label36" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="hotelname5" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label37" runat="server" Text="Address" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="address5" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox>
                                                                <asp:Label ID="Label38" runat="server" Text="Contact Number" Style="margin-left: 50px;" > </asp:Label>
                                                                <asp:TextBox ID="phone5" runat="server" Width="300px" Style="margin-left: 50px;" ></asp:TextBox> <br /> <br />
                                                                <asp:Label ID="Label39" runat="server" Text="Duration of Stay" Style="margin-left: 40px;"></asp:Label> <br />
                                                                <asp:Label ID="Label40" runat="server" Text="From:" Style="margin-left: 150px;"></asp:Label> 
                                                                <asp:TextBox ID="from5" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                <asp:Label ID="Label41" runat="server" Text="To:" Style="margin-left: 50px;"></asp:Label>                                                                 
                                                                <asp:TextBox ID="to5" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox>
                                                            </div>

                                                            <div class="card-block" style="display:none;margin-left:50px" runat="server" id="travellerAccomodation">
                                                                <asp:Label ID="Label2" runat="server" Text="care of Traveler"></asp:Label> <br /> <br />
                                                                <asp:Label ID="Label3" runat="server" Text="Hotel Name" Style="margin-left: 40px;"></asp:Label>
                                                                <asp:TextBox ID="coTraveller" runat="server" Width="300px" Style="margin-left: 50px;" CssClass="textboxes"></asp:TextBox>
                                                            </div>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Flight Details</p>

                                                            </div>
                                                             <div class="card-block">
                                                                <asp:Label ID="Label16" runat="server" Text="Airline" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="airline" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>
                                                                <asp:Label ID="Label43" runat="server" Text="Class" Style="margin-left: 30px;"></asp:Label>
                                                                <asp:TextBox ID="travelClass" runat="server" Width="300px" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>

                                                                 <hr />
                                                             </div> 
                                                                                            <asp:Label ID="Label13" runat="server" Text="Flight Schedule" Style="margin-left: 30px;font-weight:bolder"></asp:Label>
                                                                                                <div class="card-block">
                                                                                                    <asp:Label ID="Label7" runat="server" Text="Travel Route" Style="margin-left: 20px;"></asp:Label><br />
                                                                                                    <asp:Label ID="TextBox1" runat="server" Text="Flight #" Width="100px" Style="margin-left: 23px;font-size:14px"></asp:Label>
                                                                                                    <asp:Label ID="TextBox2" runat="server" Text="Date of Departure" Width="120px" Style="margin-left: 40px;font-size:14px" TextMode="Date" CssClass="textboxes"></asp:Label> 
                                                                                                    <asp:Label ID="TextBox3" runat="server" Text="From" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                    <asp:Label ID="TextBox4" runat="server" Text="To" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                    <asp:Label ID="Label8" runat="server" Text="ETD" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>
                                                                                                    <asp:Label ID="Label11" runat="server" Text="ETA" Width="100px" Style="margin-left: 23px;font-size:14px" CssClass="textboxes"></asp:Label>

                                                                                                    <br />
                                                                                                    <asp:TextBox ID="r1Flight" runat="server" Width="100px" Style="margin-left: 25px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r1FromDate" runat="server" Width="120px" Style="margin-left: 23px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                    <asp:TextBox ID="r1From" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r1To" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r1ETD" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r1ETA" runat="server" TextMode="Time" Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:Button runat="server" ID="add2nd" class="btn btn-primary" Text="+" OnClientClick="add2Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;" id="additional2routeFields" runat="server">
                                                                                                    <asp:TextBox ID="r2Flight" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r2FromDate" runat="server" Width="120px" Style="margin-left: 23px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                    <asp:TextBox ID="r2From" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r2To" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r2ETD" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r2ETA" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                     <asp:Button runat="server" ID="add3rd" class="btn btn-primary" Text="+" OnClientClick="add3Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;" id="additional3routeFields" runat="server">
                                                                                                    <asp:TextBox ID="r3Flight" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r3FromDate" runat="server" Width="120px" Style="margin-left: 23px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                    <asp:TextBox ID="r3From" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r3To" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r3ETD" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r3ETA" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:Button runat="server" ID="add4th" class="btn btn-primary" Text="+" OnClientClick="add4Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;" id="additional4routeFields" runat="server">
                                                                                                    <asp:TextBox ID="r4Flight" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r4FromDate" runat="server" Width="120px" Style="margin-left: 23px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                    <asp:TextBox ID="r4From" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r4To" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r4ETD" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r4ETA" runat="server" TextMode="Time" Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                     <asp:Button runat="server" ID="add5th" class="btn btn-primary" Text="+" OnClientClick="add5Route();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>

                                                                                                </div>
                                                                                                <div class="card-block" style="display:none;" id="additional5routeFields" runat="server">
                                                                                                    <asp:TextBox ID="r5Flight" runat="server" Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r5FromDate" runat="server" Width="120px" Style="margin-left: 23px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:TextBox ID="r5From" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r5To" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r5ETD" runat="server" TextMode="Time"  Width="150px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                    <asp:TextBox ID="r5ETA" runat="server"  Width="100px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                  
                                                                                                </div> <br />
                                                                                                 <asp:Label ID="Label15" runat="server" Text="Car/Airport Transfers" Style="margin-left: 30px;font-weight:bolder"></asp:Label>
                                                                                                 <div class="card-block"  style="margin-left:10px" id="transferInstructions" runat="server">
                                                                                                      <asp:TextBox ID="transfer1" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer1Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button1" class="btn btn-primary" Text="+" OnClientClick="add2Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers2" runat="server">
                                                                                                      <asp:TextBox ID="transfer2" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer2Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button2" class="btn btn-primary" Text="+" OnClientClick="add3Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers3" runat="server">
                                                                                                      <asp:TextBox ID="transfer3" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer3Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button3" class="btn btn-primary" Text="+" OnClientClick="add4Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers4" runat="server">
                                                                                                      <asp:TextBox ID="transfer4" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer4Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                      <asp:Button runat="server" ID="Button4" class="btn btn-primary" Text="+" OnClientClick="add5Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                </div>
                                                                                                 <div class="card-block" style="display:none;margin-left:10px" id="transfers5" runat="server">
                                                                                                      <asp:TextBox ID="transfer5" runat="server" Width="300px" Style="margin-left: 23px;" CssClass="textboxes"></asp:TextBox>
                                                                                                      <asp:TextBox ID="transfer5Date" runat="server" Width="150px" Style="margin-left: 40px;" TextMode="Date" CssClass="textboxes"></asp:TextBox> 
                                                                                                </div>                                                                                           
                                                 <div class="card-block"> <hr />
                                                                        <asp:Label ID="Label42" runat="server" Text="Additional Remarks:" Style="margin-left: 20px;"></asp:Label> <br />
                                                                    <asp:TextBox ID="remarks" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>

                                                 </div>
                                                            <div class="card-block">
                                                                <p style="font-size: 18px; color: white; background-color: #808080; padding-top: 5px; padding-left: 5px">Others</p>
                                                            </div>
                                                             <div class="row">
                                                                 <div class="col-lg-12 col-xl-6">
                                                                     <div class="card-block" style="margin-left:50px">
                                                                        <asp:Label ID="Label65" runat="server" Text="Travel Requirements"></asp:Label> 
                                                                         <asp:CheckBoxList runat="server" ID="requirements" Height="40px" Width="300px" CellPadding="5" CellSpacing="5">
                                                                             <asp:ListItem Value="Passport"> &nbsp;  &nbsp;  Passport  </asp:ListItem>
                                                                             <asp:ListItem Value="Valid Visa">  &nbsp; &nbsp; Valid Visa </asp:ListItem>
                                                                             <asp:ListItem Value="e-Travel Registration">  &nbsp; &nbsp; eTravel System Registration </asp:ListItem>

                                                                         </asp:CheckBoxList>
                                                                     </div> 

                                                                 </div>
                                                                 <div class="col-lg-12 col-xl-6">
                                                                     <div class="card-block" style="margin-left:50px">
                                                                        <asp:Label ID="Label66" runat="server" Text="Additional Notes"></asp:Label> <br />
                                                                    <asp:TextBox ID="additionalNotes" runat="server" Width="400px" TextMode="MultiLine" Style="margin-left: 60px;" CssClass="textboxes"></asp:TextBox>

                                                                     </div> 

                                                                  </div>
                                                             </div>

                                           </div>
                                       
                                             <!--MODAL FOR REQUEST DETAILS -->
                                                                <div class="modal fade" id="requestModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog modal-lg" role="document" style="max-width: 1500px;">>
                                                                        <div class="modal-content">
                                                                            <div class="modal-header" style="background-color:#09426a">
                                                                                        <div class="card-block" style="color:black;">
                                                                                                <h5 style="color:white"> Travel Request Details</h5>                                                         
                                                                                         </div>
                                                                                <button type="button" class="close ti-close" data-dismiss="modal" aria-label="Close"> </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                <!-- Your modal content goes here -->
                                                                              <div class="card-block tab-icon">
                                                                                <div class="col-lg-12 ">
            <%--                                                                        <div class="sub-title">Tab With Icon</div>--%>
                                                                                    <!-- Nav tabs -->
                                                                                    <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link " data-toggle="tab" href="#employeeDetails" role="tab"><i class="icofont icofont-home"></i>Traveler Information</a>
                                                                                            <div class="slide"></div>
                                                                                        </li>
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link active" data-toggle="tab" href="#emprequestDetails" role="tab"><i class="icofont icofont-home"></i>Travel Information</a>
                                                                                            <div class="slide"></div>
                                                                                        </li>
                                                                                        <li class="nav-item">
                                                                                            <a class="nav-link" data-toggle="tab" href="#managerApproval" role="tab"><i class="icofont icofont-ui-user "></i>Manager Email Approval</a>
                                                                                            <div class="slide"></div>
                                                                                        </li>
   
                                                                            
                                                                                    </ul>
                                                                                    <!-- Tab panes -->
                                                                                    <div class="tab-content card-block">
                                                                                        <div class="tab-pane "  id="employeeDetails" role="tabpanel"> <br />
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

                                                                                        </div>
                                                                                        <div class="tab-pane active" id="emprequestDetails" role="tabpanel">
                                                                                            <!--TRAVEL DETAILS--> <br />
                                                                                            <div class="card-block" style="margin-left:250px">
                                                                                                <asp:Label ID="Label30" runat="server" Text="Purpose of Travel:"></asp:Label> 
                                                                                                <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 30px; border-radius: 5px;color:black;background-color:transparent;font-size:18px" BorderColor="Transparent" Enabled="false" Width="300px" ></asp:TextBox>     <br />
                                                                                                <asp:Label ID="Label14" runat="server" Text="Route Destination:"></asp:Label> 
                                                                                            </div>
                                                                               
                                                                                            <center>
  
                                                                                            <div class="card-block" style="background-color:#C7E9FE;text-align:center;border-radius:10px;width:fit-content;color:black;"> <br />
                                                                                              <div class="card-block" style="background-color:#C7E9FE;text-align:center;border-radius:10px;width:fit-content;color:black;">
                                                                                               <img src="/images/airplane.png" style="width: 50px" alt="airplane.png">
                                                                                               <asp:TextBox ID="flightOptions" runat="server"  Width="343px"  Style="border-radius: 5px;color:black;background-color:transparent;font-size:18px" BorderColor="Transparent"   Enabled="false"></asp:TextBox>                                        
                                                                                                </div> <br />
                                                                                                <div class="card-block" style="display: none" id="oneWaynput" runat="server">
                                                                                                        <div style="text-align:left">
                                                                                                             <asp:Label runat="server" Text="From" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                             <asp:Label runat="server" Text="To" style="margin-left:200px;font-size:12px"></asp:Label> <br />                                                                                           
                                                                                                        </div>
                                                                                                        <div >
                                                                                                            <asp:TextBox ID="onewayFrom" runat="server"  Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>
                                                                                                                <asp:TextBox ID="onewayTo" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px; " Enabled="false"></asp:TextBox>
                                                                                                                <asp:TextBox ID="onewayDate" runat="server" Width="150px" Style="margin-left:20px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false"></asp:TextBox>

                                                                                                        </div>
                                                                                                    </div>
                                                                                                    <div class="card-block" style="display: none" id="roundTripInput" runat="server">
                                                                                                     <div style="text-align:left">
                                                                                                             <asp:Label runat="server" Text="From:" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                             <asp:Label runat="server" Text="To:" style="margin-left:200px;font-size:12px"></asp:Label> 
                                                                                                             <asp:Label runat="server" Text="Departure:" style="margin-left:200px;font-size:12px"></asp:Label>
                                                                                                             <asp:Label runat="server" Text="Return:" style="margin-left:100px;font-size:12px"></asp:Label>

                                                                                                         <br />                                                                                           
                                                                                                        </div>   
                                                                                                        <asp:TextBox ID="round1From" runat="server"  Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                        <asp:TextBox ID="round1To" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                                        <asp:TextBox ID="round2departure" runat="server"  Width="150px" Style="margin-left:20px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                                        <asp:TextBox ID="round2return" runat="server" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                    </div>
                                                                                                    <div class="card-block" id="multipleInput" style="display: none;" runat="server">
                                                                                                        <!-- Multiple destinations flight input fields -->
                                                                                                     <div style="text-align:left">
                                                                                                             <asp:Label runat="server" Text="From:" style="margin-left:10px;font-size:12px"></asp:Label>
                                                                                                             <asp:Label runat="server" Text="To:" style="margin-left:200px;font-size:12px"></asp:Label> 
                                                                                                             <asp:Label runat="server" Text="Departure:" style="margin-left:180px;font-size:12px"></asp:Label>
                                                                                                         <br />                                                                                           
                                                                                                        </div>                                                                                           
        
                                                                                                            <div id="destination1">
                                                                                                                <!--FIRST DESTINATION-->
                                                                                                                <asp:TextBox ID="TextBox7" runat="server"  Width="200px" Enabled="false" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>
                                                                                                                <asp:TextBox ID="TextBox8" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                <asp:TextBox ID="TextBox12"  runat="server"  Width="150px" Enabled="false" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                             </div>   <br />
                                                                                                            <div id="destination2">
                                                                                                                <!--SECOND DESTINATION-->
                                                                                                                <asp:TextBox ID="TextBox9" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"  ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                                <asp:TextBox ID="TextBox10" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                <asp:TextBox ID="TextBox14" runat="server" Enabled="false" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                            </div>                                                   
                                                                                                        </div>
                                                                                                    <div class="card-block" style="display:none" id="additionalFields" runat="server">
                                                                                                             <div id="destination3"> <br />
                                                                                                                 <!--THIRD DESTINATION-->
                                                                                                                <asp:TextBox ID="TextBox15" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                                <asp:TextBox ID="TextBox17" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                <asp:TextBox ID="TextBox18" runat="server" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>

                                                                                                                </div> <br />
                                                                                                             <div id="destination4" style="display:none" runat="server">
                                                                                                                            <!--FOURTH DESTINATION-->
                                                                                                                            <asp:TextBox ID="TextBox27" Enabled="false" runat="server" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px"  ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                                            <asp:TextBox ID="TextBox29" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                            <asp:TextBox ID="TextBox30" Enabled="false" Width="150px"  runat="server" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                                </div>         <br />                                    
                                                                                                             <div id="destination5" style="display:none" runat="server">
                                                                                                                        <!--FIFTH DESTINATION-->
                                                                                                                        <asp:TextBox ID="TextBox19" runat="server" Enabled="false" Width="200px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>
                                                                                                               <i class="ti-arrow-circle-right" style="margin-left:20px;color:dodgerblue;font-size:25px;margin-left:5px"></i>

                                                                                                                 <asp:TextBox ID="TextBox21" runat="server" Width="200px" Style="margin-left:-5px;text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" Enabled="false" ></asp:TextBox>
                                                                                                                        <asp:TextBox ID="TextBox22" runat="server" Enabled="false" Width="150px" Style="text-align:center; border-radius: 5px;color:black;background-color:transparent;font-size:18px" ></asp:TextBox>

                                                                                                                </div>   

                                                                                                    </div>  <br /><br />                                                                                </div>

                                                                                            </center> <br />       

                                                                                              <!--END OF FLIGHT INFORMATION-->
                                                                                            <div class="card-block"  style="margin-left:250px">
                                                                                                 <asp:Label ID="Label63" runat="server" Text="Remarks:"></asp:Label> <br />
                                                                                                    <asp:TextBox ID="employeeRemarks" runat="server" style="width:600px ;margin-left:160px;text-align:center;border-radius:10px;color:black;background-color:transparent;font-size:18px" CssClass="textboxes"  Height="91px" Enabled="false"></asp:TextBox> 
                                                                                            </div>


                                                                                        </div>
                                                                                        <div class="tab-pane" id="managerApproval" role="tabpanel">
                                                                                            <div class="card-block" id="uploadBlock" style="display:none" runat="server">
                                                                                                    <iframe id="pdfViewer"  runat="server" style="width:100%; display:none; height:600px" frameborder="0"></iframe>

                                                                                            </div>                                                                            
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                              </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                             <asp:Button runat="server" class="btn btn-primary" Text="Submit Arrangement" ID="submitArrangement" OnClick="submitArrangement_Click" />
                                    
                                    <!-- Page-body end -->
                        </div>
                    </div>
                </div>
            </div>
            </div>
                  </div>
        </div>
</asp:Content>
