﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sendToEmail.aspx.cs" Inherits="TravelDesk.Admin.sendToEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showUpload() {
            $('#uploadModal').modal('show');
            return false; // Prevents the default behavior of the button click event

        }
    </script>
<%--<script src="https://cdn.emailjs.com/dist/email.min.js"></script>
<script type="text/javascript">
    (function () {
        emailjs.init("4R2vvYNuFdqznyjBm"); // Replace with your EmailJS user ID
    })();
</script>
<script>
    function sendEmail(receiverEmail, message, name, filePaths) {
        const emailData = {
            to_email: receiverEmail,
            to_name: name,
            from_name: "Travel Desk",
            message: message
        };

        const attachments = filePaths.map(path => ({
            fileName: path.split("/").pop(),
            mimeType: "application/pdf",
            path: path
        }));

        console.log("Email Data:", emailData);
        console.log("Attachments:", attachments);

        emailjs.send("service_6updv5w", "template_p46ovxf", emailData, { attachments })
            .then(function (response) {
                alert("Email sent successfully");
                console.log('Email Sent!', response);
            }, function (error) {
                console.error("Email send failed:", error);
            });
    }
</script>--%>

    <script src="https://cdn.emailjs.com/dist/email.min.js"></script>
<script type="text/javascript">
    (function () {
        emailjs.init("4R2vvYNuFdqznyjBm"); // Replace with your EmailJS user ID
    })();
</script>
<script>
    function sendEmail(receiverEmail, message, name, filePaths) {
        const emailData = {
            to_email: receiverEmail,
            to_name: name,
            from_name: "Travel Desk",
            message: message
        };
        //const attachments = filePaths.map(path => ({
        //    fileName: path.split("/").pop(),
        //    mimeType: "application/pdf",
        //    path: path
        //}));

        console.log("Email Data:", emailData);
        //console.log("Attachments:", attachments);

        emailjs.send("service_6updv5w", "template_p46ovxf", emailData)
        //emailjs.send("service_6updv5w", "template_p46ovxf", emailData, { attachments })
            .then(function (response) {
                console.log('Email Sent!', response);
                window.location.replace("emailSent.aspx"); // Replace with your desired URL
            }, function (error) {
                alert("Email send failed");
                console.error("Email send failed:", error);
            });
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
                                                        <h5 style="color:white">Travel Arrangement</h5>
                                                    </div>  <br /><br />
                                                 <center>
                                                 <div class="card-block" style="background-color:gainsboro; width: 956px; border-radius:10px; height: 257px;">
                                                                <asp:Label ID="Label18" runat="server" style="margin-left:-530px">Recipient: </asp:Label> 
                                                                <asp:Label ID="travellerEmail" style="font-size:18px;" runat="server"></asp:Label>  <br />
                                                        
                                                                <asp:Label ID="Label2" runat="server" style="margin-left:-690px">Message: </asp:Label>                                                       <br />

                                                     <asp:TextBox runat="server" ID="emailMessage" TextMode="MultiLine" Height="105px" Width="648px" ></asp:TextBox> <br /> <br />
                                                           <asp:LinkButton runat="server" ID="attachFiles"  class="btn btn-primary"  style="color:white;font-size:14px;border-radius:10px;margin-left:-650px" OnClientClick="showUpload(); return false"> <i class="ti-link" style="color:WHITE"></i> Attach </asp:LinkButton>   <br />

                                                 </div>
                                                 </center>


                                                              <div class="card-block">

                                                                <asp:Label ID="attached" runat="server" style="display:none"> Attachments:</asp:Label> <br />
                                                            <asp:LinkButton runat="server" ID="deleteFiles"  class="btn btn-danger" style="color:white;font-size:16px;border-radius:5px;display:none"  OnClick="deleteFiles_Click"> <i class="ti-trash" style="color:white"></i> </asp:LinkButton>                                                                      

                                                                <div runat="server" id="pdfPlaceholder" >
                                                                </div>
                                                              </div>        
                                                        <div class="card-block">
                                                            <center>
                                                           <asp:LinkButton runat="server" ID="sendEmail"  class="btn btn-primary" style="color:white;font-size:16px;border-radius:10px" OnClick="sendEmail_Click" > <i class="ti-email" style="color:white"></i> Send </asp:LinkButton>     

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
