<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InternationalReport.aspx.cs" Inherits="TravelDesk.Admin.InternationalReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<%--<script type="text/javascript">
    function validateDates() {
        var startDate = document.getElementById('<%= txtStartDate.ClientID %>').value;
        var endDate = document.getElementById('<%= txtEndDate.ClientID %>').value;

        // Validate date format (yyyy-MM-dd) using regular expression
        var dateRegex = /^\d{4}-\d{2}-\d{2}$/;

        if (!dateRegex.test(startDate) || !dateRegex.test(endDate)) {
            alert("Please enter dates in the format yyyy-MM-dd.");
            return false;
        }

        return true;
    }
</script>--%>


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

                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker" TextMode="Date" DataFormatString="yyyy-MM-dd"></asp:TextBox>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker" TextMode="Date" DataFormatString="yyyy-MM-dd"></asp:TextBox>
                                        <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-primary" Text="Filter" OnClick="btnFilter_Click" />

                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="103%" BackColor="White" style="background-color:transparent; margin-left:-30px" CssClass="m-l-0">
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
