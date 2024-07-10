<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="sendToEmail.aspx.cs" Inherits="TravelDesk.Admin.sendToEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showUpload() {
            $('#uploadModal').modal('show');
            return false; // Prevents the default behavior of the button click event

        }
        function showPreview() {
            $('#previewModal').modal('show');
            return false; // Prevents the default behavior of the button click event

        }
        function deleteFile(filePath) {
            // Set the hidden field value to the file path
            document.getElementById('<%= hiddenFilePath.ClientID %>').value = filePath;

    // Trigger the postback to delete the file
            __doPostBack('<%= deleteFileButton.UniqueID %>', '');
        }

    </script>
      
    <script src="https://cdn.emailjs.com/dist/email.min.js"></script>
<script type="text/javascript">
    (function () {
        emailjs.init("7kzw_508bI-BkJqPm"); // TravelDesk
        //emailjs.init("4R2vvYNuFdqznyjBm"); // Personal Email JS

    })();
    
</script>
<script>
    function generateEmailPreview(emailData) {
        return `
                <pre>
        ${emailData.message}

        ${emailData.fileLinks}

        Please do not hesitate to reach out to me if you have any questions or require further information.

        Best Regards,
        Travel Desk
                </pre>
            `;
    }

    function displayEmailPreview(receiverEmail, message, name, formattedLinks) {
        const emailData = {
            to_email: receiverEmail,
            to_name: name,
            from_name: "Travel Desk",
            message: message,
            fileLinks: formattedLinks !== "" ? "Attached Files:\n " + formattedLinks : ""
        };

        // Generate and display email preview
        const emailPreview = generateEmailPreview(emailData);
        document.getElementById('emailPreview').innerHTML = emailPreview;

        // Store emailData in a hidden element or a global variable for sending later
        document.getElementById('hiddenEmailData').value = JSON.stringify(emailData);

        // Show the modal
        $('#previewModal').modal('show');
    }

    function sendEmailWithDriveLinks() {
        const emailData = JSON.parse(document.getElementById('hiddenEmailData').value);

        emailjs.send("service_rwuwsiv", "template_pofgyf1", emailData) // Personal Email JS
            .then(function (response) {
                console.log('Email Sent!', response);
                window.location.replace("emailSent.aspx"); // Replace with your desired URL
            }, function (error) {
                alert("Email send failed");
                console.error("Email send failed:", error);
            });
    }

    //function sendEmailWithDriveLinks(receiverEmail, message, name, formattedLinks) {
    //    const emailData = {
    //        to_email: receiverEmail,
    //        to_name: name,
    //        from_name: "Travel Desk",
    //        message: message,
    //        fileLinks: formattedLinks
    //    };

    //    if (formattedLinks !== "") {
            
    //        emailData.fileLinks = "Attached Files:\n " + formattedLinks;
    //    }

    //    console.log("Email Data:", emailData);

    //    emailjs.send("service_6updv5w", "template_p46ovxf", emailData) // Personal Email JS
    //        .then(function (response) {
    //            console.log('Email Sent!', response);
    //            window.location.replace("emailSent.aspx"); // Replace with your desired URL
    //        }, function (error) {
    //            alert("Email send failed");
    //            console.error("Email send failed:", error);
    //        });
    //}
</script>
<script src="https://alcdn.msauth.net/browser/2.18.0/js/msal-browser.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
<%--<script>
    const msalConfig = {
        auth: {
            clientId: '012c9319-8466-48bf-b2a7-addbc886747d',
            authority: 'https://login.microsoftonline.com/f8cdef31-a31e-4b4a-93e4-5f571e91255a',
            redirectUri: window.location.origin
        }
    };

    const msalInstance = new msal.PublicClientApplication(msalConfig);

    const loginRequest = {
        scopes: ["Files.ReadWrite.All", "User.Read"]
    };

    function uploadFilesToOneDrive() {
        msalInstance.loginPopup(loginRequest)
            .then(loginResponse => {
                const account = loginResponse.account;
                msalInstance.setActiveAccount(account);
                return msalInstance.acquireTokenSilent(loginRequest);
            })
            .then(tokenResponse => {
                const accessToken = tokenResponse.accessToken;
                const files = document.getElementById('<%= attachments.ClientID %>').files;

                const fileLinks = [];
                const uploadPromises = [];

                for (let i = 0; i < files.length; i++) {
                    uploadPromises.push(uploadFileToOneDrive(files[i], accessToken, fileLinks));
                }

                Promise.all(uploadPromises).then(() => {
                    document.getElementById('<%= hiddenFileLinks.ClientID %>').value = JSON.stringify(fileLinks);
                    alert('Files uploaded to onedrive successfully');
                });
            })
            .catch(error => {
                console.error(error);
                alert('Error logging in or acquiring token.');
            });
    }

    function uploadFileToOneDrive(file, accessToken, fileLinks) {
        return new Promise((resolve, reject) => {
            const fileReader = new FileReader();

            fileReader.onload = function (event) {
                const arrayBuffer = event.target.result;

                axios.put(`https://graph.microsoft.com/v1.0/me/drive/root:/${file.name}:/content`, arrayBuffer, {
                    headers: {
                        'Authorization': `Bearer ${accessToken}`,
                        'Content-Type': file.type
                    }
                })
                    .then(response => {
                        console.log('File uploaded:', response.data);

                        // Make sure response.data points to the correct property containing the URL
                        const fileLink = response.data?.webUrl; // Use optional chaining to avoid errors
                        if (fileLink) {
                            fileLinks.push(fileLink);
                            resolve();
                        } else {
                            console.error('Error: Unable to retrieve OneDrive URL.');
                            reject();
                        }
                    })
                    .catch(error => {
                        console.error('Error uploading file:', error);
                        reject();
                    });
            };

            fileReader.readAsArrayBuffer(file);
        });
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
                                             <div  class="card" style="color:black;background-color:gainsboro"> 
                                                    <div class="card-header" style="background-color:#09426a">
                                                        <h5 style="color:white">Travel Arrangement</h5>
                                                    </div>  <br /><br />
                                                                <asp:Label ID="Label18" runat="server" style="margin-left:50px">Recipient: </asp:Label> 
                                                                <asp:Label ID="travellerEmail" style="font-size:18px;margin-left:50px" runat="server"></asp:Label>  <br />
                                                        
                                                                <asp:Label ID="Label2" runat="server" style="margin-left:50px">Message: </asp:Label>      
                                                                 <asp:TextBox runat="server" ID="emailMessage" TextMode="MultiLine" Height="540px" style="margin-left:40px;width:auto;margin-right:40px"></asp:TextBox> <br /> <br />

                                                              <div class="card-block">
                                                                    <asp:Label ID="attached" runat="server" style="display:none"> Attachments:</asp:Label> <br />

                                                                <asp:PlaceHolder ID="fileListPlaceholder" runat="server"></asp:PlaceHolder>
                                                                <asp:HiddenField ID="hiddenFilePath" runat="server" />
                                                                <asp:HiddenField ID="hiddenFileLinks" runat="server" />
                                                                  <asp:Button ID="deleteFileButton" runat="server" Text="Delete File" Style="display: none;" OnClick="DeleteFileButton_Click" />
                                                              </div>        
                                                        <div class="card-block" style="background-color:white">
                                                             <asp:LinkButton runat="server" ID="sendEmailpreview"  class="btn btn-primary" style="color:black;font-size:16px;border-radius:10px;background-color:gainsboro;border-color:transparent" OnClick="sendEmail_Click" > <i class="ti-email" style="color:black"></i> Send </asp:LinkButton>     
                                                           <asp:LinkButton runat="server" ID="attachFiles"  class="btn btn-primary"  style="color:black;font-size:16px;border-radius:10px;background-color:gainsboro;border-color:transparent" OnClientClick="showUpload(); return false"> <i class="ti-link" style="color:black"></i> Attach </asp:LinkButton>   <br />


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
                                                                                </center>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                                                <div class="modal fade" id="previewModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" >
                                                                    <div class="modal-dialog" role="document" style="max-width:max-content;max-height:-100px;color:black;font-size:20px;font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title">Email Preview</h5>
                                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                                    <span aria-hidden="true">&times;</span>
                                                                                </button>                                                                         
                                                                            </div>
                                                                            <div class="modal-body">
                                                                                    <div id="emailPreview"></div>
                                                                                    <input type="hidden" id="hiddenEmailData">
                                                                                    <asp:LinkButton runat="server" ID="sendEmail"  class="btn btn-primary" style="color:black;font-size:16px;border-radius:10px;background-color:gainsboro;border-color:transparent" OnClientClick="sendEmailWithDriveLinks(); return false;"> <i class="ti-email" style="color:black"></i> Send </asp:LinkButton>     

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
