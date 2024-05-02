<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="ListofRequests.aspx.cs" Inherits="TravelDesk.Employee.ListofRequests" %>
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
                                       <div class="page-body">
                                            <asp:GridView CssClass="table container" ID="purchaseView" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellSpacing="2" ForeColor="Black">               
                                                <Columns>             
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
<%--                                                        <asp:CheckBox runat="server" Style="font-size: 18px" ID="select" />--%>
                                                        <asp:Button runat="server" Text="View" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" ID="viewDetails" OnClick="viewDetails_Click"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="travelApprovalStat" HeaderText="Status" SortExpression="travelApprovalStat" />  
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" SortExpression="travelRequestID" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Travel" SortExpression="travelType" /> 
                                                    <asp:BoundField DataField="travelDestination" HeaderText="Destination" SortExpression="travelDestination" />          
                                                    <asp:BoundField DataField="travelDeparture" HeaderText="Departure date" SortExpression="travelDeparture" DataFormatString="{0:d}"  />
                                                    <asp:BoundField DataField="travelReturn" HeaderText="Return date" SortExpression="travelReturn" DataFormatString="{0:d}"  />
                                                    <asp:BoundField DataField="travelPurpose" HeaderText="Purpose" SortExpression="travelPurpose" />
                                                    <asp:BoundField DataField="travelDesignation" HeaderText="Designation" SortExpression="travelDesignation" />
                                                </Columns>
                
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />                                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" />
                                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB_TravelDesk %>" 
                                                SelectCommand="SELECT travelRequest.travelApprovalStat, travelRequest.travelRequestID, travelRequest.travelType, travelRequest.travelDestination, travelRequest.travelDeparture, travelRequest.travelReturn, travelRequest.travelPurpose, travelRequest.travelDesignation FROM travelRequest WHERE (travelRequest.travelUserID = @userID)">
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
