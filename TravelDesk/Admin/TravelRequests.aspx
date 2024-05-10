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
                                            <asp:GridView CssClass="table container" class="table-hover" ID="travelRequests" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False" CellSpacing="2" ForeColor="Black" OnRowDataBound="travelRequests_RowDataBound">               
                                                <Columns>   
                                                    <asp:TemplateField HeaderText="Request ID">
                                                        <ItemTemplate>
                                                        <asp:Button runat="server" Style="background-color: transparent; font-size: 15px;" class="active btn waves-effect text-center" ID="btnRequestID" OnClick="viewDetails_Click"/>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="travelReqStatus" HeaderText="Status" />
                                                    <asp:BoundField DataField="travelType" HeaderText="Type of Request" />
                                                    <asp:BoundField DataField="FullName" HeaderText="Traveller Name" />
                                                    <asp:BoundField DataField="travelDestination" HeaderText="Destination" />
                                                    <asp:BoundField DataField="travelDU" HeaderText="Department Unit" />
                                                    <asp:BoundField DataField="travelProjectCode" HeaderText="Project Code" />
                                                    <asp:BoundField DataField="travelDateSubmitted" HeaderText="Date Submitted" DataFormatString="{0:MMMM dd, yyyy}" />


                                                    
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
