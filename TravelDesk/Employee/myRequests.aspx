﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEmployee.Master" AutoEventWireup="true" CodeBehind="myRequests.aspx.cs" Inherits="TravelDesk.Employee.myRequests" %>
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
                                          <li class="breadcrumb-item"><a href="Employee/EmployeeDashboard">Dashboard</a>
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
                                                    <div class="card-block">
                                                        <asp:LinkButton runat="server" Text="Drafts" class="btn btn-primary" ID="viewDrafts" OnClick="viewDrafts_Click"></asp:LinkButton> <br />
                                                    </div> <br />
                                            <asp:GridView CssClass="table container" ID="travelRequests" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" CellSpacing="2" ForeColor="Black">               
                                                <Columns>   
                                                    <asp:BoundField DataField="travelReqStatus" HeaderText="Request Status" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Travel Type" />
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" />
                                                    <asp:BoundField DataField="travelUserID" HeaderText="User ID" />
                                                    <asp:BoundField DataField="travelDateSubmitted" HeaderText="Date Submitted" />
                                                    <asp:BoundField DataField="travelHomeFacility" HeaderText="Home Facility" />
                                                    <asp:BoundField DataField="travelProjectCode" HeaderText="Project Code" />
                                                    <asp:BoundField DataField="travelFrom" HeaderText="Origin" />
                                                    <asp:BoundField DataField="travelTo" HeaderText="Destination" />
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
