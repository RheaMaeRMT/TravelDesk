<%@ Page Title="" Language="C#" MasterPageFile="~/ApproverSite.Master" AutoEventWireup="true" CodeBehind="myEmployeeRequests.aspx.cs" Inherits="TravelDesk.Approver.myEmployeeRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                  <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div class="page-header">
                          <div class="page-block">
                              <div class="row align-items-center">
                                  <div class="col-md-8">
                                      <div class="page-header-title">
                                          <h5 class="m-b-10">TRAVEL REQUESTS</h5>
                                      </div>
                                  </div>
                                  <div class="col-md-4">
                                      <ul class="breadcrumb-title">
                                          <li class="breadcrumb-item">
                                              <a href="index.html"> <i class="fa fa-home"></i> </a>
                                          </li>
                                          <li class="breadcrumb-item"><a href="Approver/ApproverDashboard">Dashboard</a>
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
                                            <asp:GridView CssClass="table container" ID="purchaseView" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellSpacing="2" ForeColor="Black">               
                                                <Columns>             
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
<%--                                                        <asp:CheckBox runat="server" Style="font-size: 18px" ID="select" />--%>
                                                        <asp:Button runat="server" Text="View" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" ID="viewDetails"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" SortExpression="travelRequestID" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Travel" SortExpression="travelType" /> 
                                                    <asp:BoundField DataField="travelDeparture" HeaderText="Departure date" SortExpression="travelDeparture" DataFormatString="{0:d}"  />
                                                    <asp:BoundField DataField="travelReturn" HeaderText="Return date" SortExpression="travelReturn" DataFormatString="{0:d}"  />
                                                    <asp:BoundField DataField="travelPurpose" HeaderText="Purpose" SortExpression="travelPurpose" />
                                                </Columns>
                
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" /> 
                                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB_TravelDesk %>" 
                                                SelectCommand="SELECT travelRequest.travelRequestID, travelRequest.travelType, travelRequest.travelDeparture, travelRequest.travelReturn, travelRequest.travelPurpose FROM travelRequest WHERE (travelRequest.travelManager = @userName)">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="userName" SessionField="userName" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>


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

