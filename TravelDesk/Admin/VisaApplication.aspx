<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisaApplication.aspx.cs" Inherits="TravelDesk.Admin.VisaApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
               <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/visaRequests.png" style="width: 250px;" alt="logo.png">

                                </div>

                      </div>
                      <!-- Page-header end -->

                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                    <div class="page-body">
                                            <asp:GridView CssClass="table container" ID="visaRequests" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" CellSpacing="2" ForeColor="Black">               
                                                <Columns> 
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                        <asp:Button runat="server" Text="View" Style="background-color: transparent; font-size: 16px;" class="active btn waves-effect text-center" ID="viewDetails"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="visaReqStatus" HeaderText="Request Status" />
                                                    <asp:BoundField DataField="visaReqID" HeaderText="Request ID" />
                                                    <asp:BoundField DataField="FullName" HeaderText="Traveller Name" />
                                                    <asp:BoundField DataField="visaPurpose" HeaderText="Purpose" />
                                                    <asp:BoundField DataField="visaDestination" HeaderText="Destination" />
                                                    <asp:BoundField DataField="visaEstTravelDate" HeaderText="est. Travel Date" />
                                                    <asp:BoundField DataField="visaDU" HeaderText="Department Unit" />
                                                    <asp:BoundField DataField="visaBdate" HeaderText="Birthdate" />
                                                    <asp:BoundField DataField="visaEmail" HeaderText="Traveller Email" />
                                                    <asp:BoundField DataField="visaLevel" HeaderText="Level" />
                                                    <asp:BoundField DataField="visaReqSubmitted" HeaderText="Date Submitted" />



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
