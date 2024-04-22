<%@ Page Title="" Language="C#" MasterPageFile="~/ApproverSite.Master" AutoEventWireup="true" CodeBehind="myRequests.aspx.cs" Inherits="TravelDesk.Approver.myRequests" %>
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
                                                    <asp:BoundField DataField="travelReqStatus" HeaderText="Status" SortExpression="travelReqStatus" />  
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" SortExpression="travelRequestID" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Travel" SortExpression="travelType" /> 
                                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />          
                                                    <asp:BoundField DataField="travelPurpose" HeaderText="Purpose" SortExpression="travelPurpose" />
                                                    <asp:BoundField DataField="travelDU" HeaderText="Designation" SortExpression="travelDU" />
                                                    <asp:BoundField DataField="travelProjectCode" HeaderText="Project Code" SortExpression="travelProjectCode" />
                                                    <asp:BoundField DataField="travelHomeFacility" HeaderText="Home Facility" SortExpression="travelHomeFacility" />

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
                                                SelectCommand="SELECT travelRequest.travelReqStatus, travelRequest.travelRequestID, travelRequest.travelType,travelRequest.travelFname + ' ' + ISNULL(travelRequest.travelMname, '') + ' ' + travelRequest.travelLname AS FullName, travelRequest.travelPurpose, travelRequest.travelDU, travel.travelProjectCode, travel.travelHomeFacility FROM travelRequest WHERE (travelRequest.travelUserID = @userID)">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="userID" SessionField="userID" />
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
