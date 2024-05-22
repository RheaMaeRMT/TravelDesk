<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sendToEmail.aspx.cs" Inherits="TravelDesk.Admin.sendToEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showUpload() {
            $('#uploadModal').modal('show');
            return false; // Prevents the default behavior of the button click event

        }
    </script>
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
                                         <div class="page-body" style="color:black;font-size:16px;">
                                             <div  class="card" style="color:black;background-color:white"> 
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Email Traveller</h5>
                                                    </div> 
                                                        <div class="card-block">      
                                                            <center>
                                                                <asp:Label ID="Label18" runat="server"> Sending Email to: </asp:Label> <br />
                                                                <asp:Label ID="travellerEmail" style="font-size:20px" runat="server"></asp:Label> 

                                                            </center>
                                                        </div>

                                                              <div class="card-block">

                                                                <asp:Label ID="attached" runat="server" style="display:none"> Attachments:</asp:Label> <br />
                                                            <asp:LinkButton runat="server" ID="deleteFiles"  class="btn btn-danger" style="color:white;font-size:16px;border-radius:5px;display:none"  OnClick="deleteFiles_Click"> <i class="ti-trash" style="color:white"></i> </asp:LinkButton>                                                                      

                                                                <div runat="server" id="pdfPlaceholder" >
                                                                </div>
                                                              </div>        
                                                        <div class="card-block">
                                                            <center>
                                                           <asp:LinkButton runat="server" ID="attachFiles"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px" OnClientClick="showUpload(); return false"> <i class="ti-files" style="color:white"></i> Attach Files </asp:LinkButton>     
                                                           <asp:LinkButton runat="server" ID="sendEmail"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px" > <i class="ti-email" style="color:white"></i> Send </asp:LinkButton>     

                                                            </center>

                                                        </div>
                                             </div>

                                         </div>
                                                                <div class="modal fade" id="uploadModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                                    <div class="modal-dialog modal-md" role="document" style="max-width: 500px;">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title"> Attachments</h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                  <asp:Label ID="Label42" runat="server" Text="Insert Attachments" style="margin-left:50px;font-size:16px;color:black"></asp:Label>  <br /> <br />                                                                                                           

                                                                                <center>
                                                                                <div class="card-block" style="font-size:16px">
                                                                                    <asp:FileUpload ID="attachments" AllowMultiple="true" runat="server" style="margin-left:80px" /> <br /> <br />       
                                                                                    <asp:LinkButton runat="server" ID="uploadButton" class="btn btn-primary" style="color:white;font-size:16px;border-radius:20px;width:160px;margin-left:50px" OnClick="uploadButton_Click"> <i class="ti-upload" style="color:white"></i>Upload</asp:LinkButton>    

                                                                                </div>
                                                                                    <div runat="server" id="Div1">

                                                                                    </div>
                                                                                </center>

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
