﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="myDraftTravelRequests.aspx.cs" Inherits="TravelDesk.Employee.myDraftTravelRequests" %>
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
<br />
                                            <asp:GridView CssClass="table container" style="text-align:center" class="table-hover" ID="travelRequests" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" CellSpacing="2" ForeColor="Black" OnRowDataBound="travelRequests_RowDataBound">               
                                                <Columns>   
                                                     <asp:TemplateField HeaderText="Request ID">
                                                        <ItemTemplate>
                                                              <asp:Button runat="server" Style="background-color: transparent; font-size: 15px;" class="active btn waves-effect text-center" ID="btnRequestID" OnClick="viewDetails_Click"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="travelReqStatus" HeaderText="Status" SortExpression="travelReqStatus" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Type of Request" SortExpression="travelType"/>
                                                    <asp:BoundField DataField="FullName" HeaderText="Traveler Name" SortExpression="FullName" />
                                                    <asp:BoundField DataField="travelDestination" HeaderText="Destination" SortExpression="travelDestination"/>
                                                    <asp:BoundField DataField="travelDates" HeaderText="Travel Dates" DataFormatString="{0:MMMM dd, yyyy}" SortExpression="travelDates"  />
                                                    <asp:BoundField DataField="travelDU" HeaderText="Department Unit" SortExpression="travelDU"/>
                                                    <asp:BoundField DataField="travelProjectCode" HeaderText="Project Code" SortExpression="travelProjectCode"/>
                                                    <asp:BoundField DataField="travelDateSubmitted" HeaderText="Date Submitted"  DataFormatString="{0:MMMM dd, yyyy}" SortExpression="travelDateSubmitted" />


                                                    
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
