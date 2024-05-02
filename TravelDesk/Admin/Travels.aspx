<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Travels.aspx.cs" Inherits="TravelDesk.Admin.Travels" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
               <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/travelReport.png" style="width: 250px;" alt="logo.png">

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
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" SortExpression="travelRequestID" />
                                                    <asp:BoundField DataField="travallerName" HeaderText="Traveller Name" SortExpression="travallerName" />
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
                                                SelectCommand="SELECT travelRequest.travelRequestID, CONCAT(travelRequest.travelFname, ' ', travelRequest.travelMname, ' ', travelRequest.travelLname) AS travallerName FROM travelRequest">
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
