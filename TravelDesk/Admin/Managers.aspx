<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="TravelDesk.Admin.Managers" %>
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
                                          <h5 class="m-b-10">MANAGERS</h5>
                                      </div>
                                  </div>
                                  <div class="col-md-4">
                                      <ul class="breadcrumb-title">
                                          <li class="breadcrumb-item">
                                              <a href="index.html"> <i class="fa fa-home"></i> </a>
                                          </li>
                                          <li class="breadcrumb-item"><a href="AdminDashboard.aspx">Dashboard</a>
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
                                        <div class="page-body" style="color:black;">
                                            <asp:Button runat="server" Text="Add new Approver" class="btn btn-primary" ID="newApproverbtn" OnClientClick="showModal(); return false;" />

                                            <!-- MODAL FOR THE add new approver -->
                                            <div class="modal fade" id="noModal" tabindex="-1" role="dialog" aria-labelledby="newApproverModal" aria-hidden="true">
                                                <div class="modal-dialog modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="noModalLabel">New Approver</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                    <!--EMPLOYEE DETAILS-->
                                                                <div class="card-block">
                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                                </div>
                                                            <div style="margin-left:50px">
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                    <asp:TextBox ID="approverCompanyID" runat="server" style="margin-left:80px" Width="345px"></asp:TextBox> <br /> <br />
                                                                    <asp:Label ID="Label7" runat="server" Text="Employee Name" ></asp:Label>
                                                                    <asp:TextBox ID="approverName" runat="server"  Width="345px" style="margin-left:55px"></asp:TextBox> <br /> <br />
                                                                </div>
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label9" runat="server" Text="Mobile Number"  ></asp:Label>
                                                                    <asp:TextBox ID="approverPhone" runat="server"  Width="345px" CssClass="auto-style7" style="margin-left:55px"></asp:TextBox> <br /> <br />
                                                                    <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
                                                                    <asp:TextBox ID="approverEmail" runat="server"  CssClass="auto-style4" Width="345px" style="margin-left:120px"></asp:TextBox> <br /> <br />
                                                                </div>
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label4" runat="server" Text="Manager"  ></asp:Label>
                                                                    <asp:TextBox ID="approverManager" runat="server"  Width="345px" CssClass="auto-style7" style="margin-left:100px"></asp:TextBox> <br /> <br />
                                                                    <asp:Label ID="Label3" runat="server" Text="Department Unit"></asp:Label>
                                                                    <asp:TextBox ID="approverDU" runat="server"  CssClass="auto-style4" Width="345px" style="margin-left:50px"></asp:TextBox> <br /> <br />
                                                                </div>
                                                                <div class="card-block">
                                                                    <asp:Label ID="Label6" runat="server" Text="Level"></asp:Label>
                                                                    <asp:TextBox ID="approverLevel" runat="server"  CssClass="auto-style4" Width="345px" style="margin-left:120px"></asp:TextBox> <br /> <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button runat="server" class="btn btn-primary" ID="addBtn" Text="Add" OnClick="addBtn_Click" />
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
                                            <!-- Modal -->
                                                <div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                  <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                      <div class="modal-header">
                                                        <h5 class="modal-title" id="exampleModalLabel">Success!</h5>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                          <span aria-hidden="true">&times;</span>
                                                        </button>
                                                      </div>
                                                      <div class="modal-body">
                                                        New Approver Successfully Added!<br>
                                                        Account Credentials:<br>
                                                        Email: <span id="approverEmaildone"></span><br>
                                                        Company ID: <span id="approverCompanyIDdone"></span>
                                                      </div>
                                                      <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                      </div>
                                                    </div>
                                                  </div>
                                                </div>

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
