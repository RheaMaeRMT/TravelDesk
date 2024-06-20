<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="billingInformation.aspx.cs" Inherits="TravelDesk.Admin.billingInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript">
        //HOTEL BUTTONS FUNCTIONS
        function add2Hotel(button) {
            document.getElementById('<%=hotel2.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button5.ClientID%>').style.display = 'none';
        }
        function add3Hotel(button) {
            document.getElementById('<%=hotel3.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button1.ClientID%>').style.display = 'none';
        }
        function add4Hotel(button) {
            document.getElementById('<%=hotel4.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button2.ClientID%>').style.display = 'none';
        }
        function add5Hotel(button) {
            document.getElementById('<%=hotel5.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button3.ClientID%>').style.display = 'none';
            document.getElementById('<%=Button4.ClientID%>').style.display = 'none';

        }

        //PLANE BUTTON FUNCTIONS
        function add2plane(button) {
            document.getElementById('<%=plane2.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button6.ClientID%>').style.display = 'none';
        }
        function add3plane(button) {
            document.getElementById('<%=plane3.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button7.ClientID%>').style.display = 'none';
        }
        function add4plane(button) {
            document.getElementById('<%=plane4.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button8.ClientID%>').style.display = 'none';
        }
        function add5plane(button) {
            document.getElementById('<%=plane5.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button9.ClientID%>').style.display = 'none';
            document.getElementById('<%=Button10.ClientID%>').style.display = 'none';
        }

        //TRANSFERS BUTTON FUNCTIONS
        function add2Transfer(button) {
            document.getElementById('<%=transfers2.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button12.ClientID%>').style.display = 'none';
        }
        function add3Transfer(button) {
            document.getElementById('<%=transfers3.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button13.ClientID%>').style.display = 'none';
        }
        function add4Transfer(button) {
            document.getElementById('<%=transfers4.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button14.ClientID%>').style.display = 'none';
        }
        function add5Transfer(button) {
            document.getElementById('<%=transfers5.ClientID %>').style.display = 'block';
            document.getElementById('<%=Button15.ClientID%>').style.display = 'none';
            document.getElementById('<%=Button16.ClientID%>').style.display = 'none';
        }

    </script>
    <style type="text/css">
        .auto-style2 {
            left: -1px;
            top: 0px;
        }
        .auto-style5 {
            left: 0px;
            top: 0px;
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
                                                <div class="card" style="color:black;background-color:white">
                                                    <div class="card-header" style="background-color:#09426a">
                                                                <asp:Label runat="server" ID="billingLabel" style="color:white; font-size:16px;margin-left:10px;" CssClass="h5"></asp:Label>
                                                    </div> 
                                                            <div class="card-block tab-icon">
                                                                    <div class="auto-style2">
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
                                                                                        <div class="row" style="margin-left:25px" >
                                                                                            <div class="auto-style5" style="background-color:#0a426a;">
                                                                                                 <div class="card-block" style="width: 731px" >
                                                                                                    <asp:Label ID="Label16" runat="server" Text="Hotel Charges:" Style="margin-left: 30px;color:white"></asp:Label> <br />
                                                                                                    <asp:TextBox ID="hotelCharges" runat="server" Width="300px" Text="₱" Style="margin-left: 160px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button5" class="btn btn-primary" Text="+" OnClientClick="add2Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px;"/>
                                                                                                 </div> 
                                                                                                 <div class="card-block" id="hotel2" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label2" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelCharges2" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button1" class="btn btn-primary" Text="+" OnClientClick="add3Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>                                                                                                  
                                                                                                 <div class="card-block" id="hotel3" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label3" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelCharges3" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button2" class="btn btn-primary" Text="+" OnClientClick="add4Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>                                                                                                 
                                                                                                 <div class="card-block" id="hotel4" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label7" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelCharges4" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button3" class="btn btn-primary" Text="+" OnClientClick="add5Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>  
                                                                                                 <div class="card-block" id="hotel5" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label8" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelCharges5" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button4" class="btn btn-primary" Text="+" OnClientClick="add6Hotel();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>   
                                                                                                <asp:Button runat="server" CssClass="btn btn-primary" id="hotelTotal" Text="Calculate Hotel Charges" style="margin-left:50px" Width="195px" OnClick="hotelTotal_Click"/> <br /> <br />

                                                                                                <center>
                                                                                                    <asp:Label runat="server" Text="TOTAL:" style="color:white"></asp:Label>
                                                                                                    <asp:TextBox ID="hotelChargesTotal" runat="server" Width="265px" Text="₱" Style="background-color:white;color:black" Enabled="false" CssClass="textboxes" placeholder="₱" Height="40px"></asp:TextBox> 
                                                                                                </center> <br /><br />
                                                                                            </div> 
                                                                                                    <div class="auto-style5" style="background-color:#0a426a;" >
                                                                                                         <div class="card-block" style="width: 732px">
                                                                                                            <asp:Label ID="Label1" runat="server" Text="Plane Fares:" Style="margin-left: 30px;color:white"></asp:Label> <br />
                                                                                                            <asp:TextBox ID="planeFare" runat="server" Width="300px" Text="₱" Style="margin-left: 160px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                            <asp:Button runat="server" ID="Button6" class="btn btn-primary" Text="+" OnClientClick="add2plane();  return false;"  CausesValidation="False" Style="margin-left: 10px;"/>
                                                                                                         </div> 
                                                                                                         <div class="card-block" id="plane2" runat="server" style="display:none">
                                                                                                            <asp:Label ID="Label9" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                            <asp:TextBox ID="planeFare2" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                            <asp:Button runat="server" ID="Button7" class="btn btn-primary" Text="+" OnClientClick="add3plane();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                         </div>                                                                                                  
                                                                                                         <div class="card-block" id="plane3" runat="server" style="display:none">
                                                                                                            <asp:Label ID="Label10" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                            <asp:TextBox ID="planeFare3" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                            <asp:Button runat="server" ID="Button8" class="btn btn-primary" Text="+" OnClientClick="add4plane();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                         </div>                                                                                                 
                                                                                                         <div class="card-block" id="plane4" runat="server" style="display:none">
                                                                                                            <asp:Label ID="Label11" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                            <asp:TextBox ID="planeFare4" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                            <asp:Button runat="server" ID="Button9" class="btn btn-primary" Text="+" OnClientClick="add5plane();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                         </div>  
                                                                                                         <div class="card-block" id="plane5" runat="server" style="display:none">
                                                                                                            <asp:Label ID="Label12" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                            <asp:TextBox ID="planeFare5" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                            <asp:Button runat="server" ID="Button10" class="btn btn-primary" Text="+" OnClientClick="add6plane();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                         </div>  
                                                                                                <asp:Button runat="server" CssClass="btn btn-primary" id="calculatePlaneFare" Text="Calculate Plane Fares" style="margin-left:50px" Width="195px" OnClick="calculatePlaneFare_Click"/>  <br /> <br />

                                                                                                <center>
                                                                                                    <asp:Label runat="server" Text="TOTAL:" style="color:white"></asp:Label>                                                                                                   
                                                                                                    <asp:TextBox ID="planeFaresTotal" runat="server" Width="265px" Text="₱" Style="background-color:white;color:black" Enabled="false" CssClass="textboxes" placeholder="₱" Height="40px"></asp:TextBox> 
                                                                                                </center> <br /><br />
                                                                                                        
                                                                                                    </div> 

                                                                                        </div> <br />
                                                                                        <div class="row" style="margin-left:25px" >
                                                                                            <div class="auto-style5" style="background-color:#0a426a;">
                                                                                                 <div class="card-block" style="width: 731px" >
                                                                                                    <asp:Label ID="Label13" runat="server" Text="Transfer Charges:" Style="margin-left: 30px;color:white"></asp:Label> <br />
                                                                                                    <asp:TextBox ID="trans1" runat="server" Width="300px" Text="₱" Style="margin-left: 160px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button12" class="btn btn-primary" Text="+" OnClientClick="add2Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px;"/>
                                                                                                 </div> 
                                                                                                 <div class="card-block" id="transfers2" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label14" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="trans2" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button13" class="btn btn-primary" Text="+" OnClientClick="add3Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>                                                                                                  
                                                                                                 <div class="card-block" id="transfers3" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label15" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="trans3" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button14" class="btn btn-primary" Text="+" OnClientClick="add4Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>                                                                                                 
                                                                                                 <div class="card-block" id="transfers4" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label17" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="trans4" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button15" class="btn btn-primary" Text="+" OnClientClick="add5Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>  
                                                                                                 <div class="card-block" id="transfers5" runat="server" style="display:none">
                                                                                                    <asp:Label ID="Label18" runat="server" Text="" Style="margin-left: 30px;"></asp:Label>
                                                                                                    <asp:TextBox ID="trans5" runat="server" Width="300px" Text="₱" Style="margin-left: 130px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                    <asp:Button runat="server" ID="Button16" class="btn btn-primary" Text="+" OnClientClick="add6Transfer();  return false;"  CausesValidation="False" Style="margin-left: 10px"/>
                                                                                                 </div>   
                                                                                                <asp:Button runat="server" CssClass="btn btn-primary" id="totalTransferCharges" Text="Calculate Transfer Charges" style="margin-left:50px" Width="216px" OnClick="totalTransferCharges_Click" /> <br /> <br />

                                                                                                <center>
                                                                                                    <asp:Label runat="server" Text="TOTAL:" style="color:white"></asp:Label>                                                                                                    
                                                                                                    <asp:TextBox ID="transfersTotal" runat="server" Width="265px" Text="₱" Style="background-color:white;color:black" Enabled="false" CssClass="textboxes" placeholder="₱" Height="40px"></asp:TextBox> 
                                                                                                </center> <br /><br />
                                                                                            </div> 
                                                                                                    <div class="auto-style5" style="background-color:#0a426a;" >
                                                                                                         <div class="card-block" style="width: 732px">
                                                                                                            <asp:Label ID="Label19" runat="server" Text="Per Diem:" Style="margin-left: 30px;color:white"></asp:Label> <br />
                                                                                                            <asp:TextBox ID="perDiem" runat="server" Width="286px" Text="₱" Style="margin-left: 160px;" CssClass="textboxes" placeholder="₱"></asp:TextBox> 
                                                                                                         </div>                                                                                                        
                                                                                                    </div> 

                                                                                        </div> <br />


                                                                                        <center>
                                                                                             <asp:Button runat="server" class="btn btn-primary" Text="Calculate" ID="calculate" CausesValidation="false" Width="100px" OnClick="calculate_Click" /> <br />
                                                                                                 <div class="card-block" style="display:none" runat="server" id="totalBlock"> <br />
                                                                                                    <asp:Label ID="Label6" runat="server" Text="Total Expenses:" style="margin-left:-120px"></asp:Label>
                                                                                                    <asp:TextBox ID="totalExpensesTxt" runat="server" Width="300px" Style="margin-left: 40px;color:black; font-size:16px" CssClass="textboxes" Enabled="false"></asp:TextBox>
                                                                                                 </div> 

                                                                                                </center>
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
