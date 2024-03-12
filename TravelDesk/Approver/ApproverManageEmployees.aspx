<%@ Page Title="" Language="C#" MasterPageFile="~/ApproverSite.Master" AutoEventWireup="true" CodeBehind="ApproverManageEmployees.aspx.cs" Inherits="TravelDesk.Approver.ApproverManageEmployees" %>
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
                                          <h5 class="m-b-10"> EMPLOYEE MANAGEMENT</h5>
                                         
                                      </div>
                                  </div>
                                  <div class="col-md-4">
                                      <ul class="breadcrumb-title">
                                          <li class="breadcrumb-item">
                                              <a href="index.html"> <i class="fa fa-home"></i> </a>
                                          </li>
                                          <li class="breadcrumb-item"><a href="ApproverDashboard.aspx">Dashboard</a>
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
                                                        <asp:LinkButton runat="server" Text="Enroll New Employee" class="btn btn-primary" OnClientClick="showModal(); return false;"></asp:LinkButton>
                                                    </div>
                                            <!-- MODAL FOR THE add new approver -->
                                            <div class="modal fade" id="noModal" tabindex="-1" role="dialog" aria-labelledby="newEmployeeModal" aria-hidden="true">
                                                <div class="modal-dialog modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="noModalLabel">New Employee</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                    <!--EMPLOYEE DETAILS-->
                                                                <div class="card-block">
                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                                </div>
                                                            <div style="margin-left:20px">
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                    <asp:TextBox ID="Int" runat="server" style="margin-left:80px" Width="345px"></asp:TextBox> <br /> <br />
                                                                    <asp:Label ID="Label7" runat="server" Text="Employee Name" ></asp:Label>
                                                                    <asp:TextBox ID="TextBox1" runat="server"  Width="345px" style="margin-left:55px"></asp:TextBox> <br /> <br />
                                                                </div>
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label3" runat="server" Text="Department Unit"></asp:Label>
                                                                    <asp:TextBox ID="TextBox2" runat="server"  CssClass="auto-style4" Width="345px" style="margin-left:50px"></asp:TextBox> <br /> <br />
                                                                    <asp:Label ID="Label9" runat="server" Text="Mobile Number"  ></asp:Label>
                                                                    <asp:TextBox ID="TextBox7" runat="server"  Width="345px" CssClass="auto-style7" style="margin-left:55px"></asp:TextBox> <br /> <br />
                                                                </div>
<%--                                                                <div class="card-block">
                                                                    <asp:Label ID="Label4" runat="server" Text="VOIP Ext."></asp:Label>
                                                                    <asp:TextBox ID="TextBox3" runat="server"  CssClass="auto-style1" Width="345px" style="margin-left:95px"></asp:TextBox> <br /> <br />
                                                                    
                                                                </div>--%>
<%--                                                                <div class="card-block">
                                                                    <asp:Label ID="Label5" runat="server" Text="Project Code"></asp:Label>
                                                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="auto-style3" Width="345px" style="margin-left:70px"></asp:TextBox>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" class="btn btn-primary" data-dismiss="modal" Text="Enroll" />
                                                            <asp:Button runat="server" class="btn btn-secondary" data-dismiss="modal" Text="Cancel" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- JavaScript to show the modal -->
                                            <script>
                                                function showModal() {
                                                    $('#noModal').modal('show');
                                                }
                                            </script>
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

