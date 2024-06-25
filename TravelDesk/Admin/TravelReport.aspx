<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelReport.aspx.cs" Inherits="TravelDesk.Admin.TravelReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
                                        <!-- Ensure ScriptManager is present -->
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="151%" BackColor="White" style="background-color:transparent; margin-left:-30px" CssClass="m-l-0">
                                        </rsweb:ReportViewer>

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
