<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelRequests.aspx.cs" Inherits="TravelDesk.Admin.TravelRequests" %>
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
                                            <asp:GridView CssClass="table container" ID="travelRequests" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" CellSpacing="2" ForeColor="Black">               
                                                <Columns> 
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                        <asp:Button runat="server" Text="View" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" ID="viewDetails" OnClick="viewDetails_Click"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="travelReqStatus" HeaderText="Request Status" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Travel Type" />
                                                    <asp:BoundField DataField="travelRequestID" HeaderText="Request ID" />
                                                    <asp:BoundField DataField="FullName" HeaderText="Traveller Name" />
                                                    <asp:BoundField DataField="travelProjectCode" HeaderText="Project Code" />
                                                    <asp:BoundField DataField="travelPurpose" HeaderText="Travel Purpose" />
                                                    <asp:BoundField DataField="travelHomeFacility" HeaderText="Home Facility" />
                                                    <asp:BoundField DataField="travelDU" HeaderText="Department Unit" />
                                                    <asp:BoundField DataField="travelEmail" HeaderText="Traveller Email" />
                                                    <asp:BoundField DataField="travelRemarks" HeaderText="Request Remarks" />
                                                    <asp:BoundField DataField="travelDateSubmitted" HeaderText="Date Submitted" />



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
