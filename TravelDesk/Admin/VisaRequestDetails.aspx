<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisaRequestDetails.aspx.cs" Inherits="TravelDesk.Admin.VisaRequestDetails" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <style>
        .txtBox{
            Width: 260px;
            margin-left:80px;
        }
        .required{
            color:red;
            font-size:14px;
        }
        .textboxes{
            color:black;
            background-color:transparent;
           
        }
        .tracker {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 50px auto;
            width: 80%;
        }

        .stage {
            text-align: center;
            position: relative;
        }

        .circle {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            background-color: #ccc; /* Default color for uncompleted stages */
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 18px;
            position: relative;
            z-index: 1;
        }

        .completed {
            background-color: #33D176; /* Green color for completed stages */

        }

        .text {
            margin-top: 10px;
        }

        .line {
            flex: 1;
           background-color: #ccc;
            height: 5px; /* Adjust line thickness */
        }

        .line.completed {
            background-color: #33D176; /* Change color for completed line */
        }
        .circle:hover::after {
            content: attr(data-hover-message); /* Display the hover message */
            position: absolute;
             top: -40px; /* Position the hover message above the circle */
            left: calc(50% + 5px); /* Position the hover message to the right of the circle */
            padding: 5px;
            text-align:center;
            width: 200px;
            background-color:lightgrey; /* Background color of the hover message */
            color: black; /* Text color of the hover message */
            font-size: 12px;
            border-radius: 5px;
            z-index: 999; /* Ensure the hover message appears above other elements */
        }


    </style>
    <script src="https://cdn.emailjs.com/dist/email.min.js"></script>
<script type="text/javascript">
    (function () {
        emailjs.init("7kzw_508bI-BkJqPm"); // TravelDesk
        //emailjs.init("4R2vvYNuFdqznyjBm"); // Personal Email JS

    })();
    
</script>
<script>
    function deleteFile(filePath) {
        // Set the hidden field value to the file path
        document.getElementById('<%= hiddenFilePath.ClientID %>').value = filePath;

                // Trigger the postback to delete the file
                __doPostBack('<%= deleteFileButton.UniqueID %>', '');
    }

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

        emailjs.send("service_rwuwsiv", "template_ewyjb8e", emailData) // Personal Email JS
            .then(function (response) {
                console.log('Email Sent!', response);
                window.location.replace("visaEmailSent.aspx"); // Replace with your desired URL
            }, function (error) {
                alert("Email send failed");
                console.error("Email send failed:", error);
            });
    }

    //function sendEmailWithDriveLinks(receiverEmail, name, message,formattedLinks) {
    //    const emailData = {
    //        to_email: receiverEmail,
    //        to_name: name,
    //        message: message,
    //        from_name: "Travel Desk"
    //    };

    //    if (formattedLinks !== "") {
            
    //        emailData.fileLinks = "Attached Files:\n " + formattedLinks;
    //    }

    //    console.log("Email Data:", emailData);

    //    emailjs.send("service_6updv5w", "template_tp5wuwk", emailData) // Personal Email JS
    //        .then(function (response) {
    //            console.log('Email Sent!', response);
    //            alert("Email Sent !");
    //            window.location.replace("VisaBilling.aspx"); // Replace with your desired URL
    //        }, function (error) {
    //            alert("Email send failed");
    //            console.error("Email send failed:", error);
    //        });
    //}
</script>
<script src="https://alcdn.msauth.net/browser/2.18.0/js/msal-browser.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
            <div class="pcoded-main-container">
              <div class="pcoded-wrapper">
                  <div class="pcoded-content">
                      <!-- Page-header start -->
                      <div style="background-color:white">
                                 <div>
                                    <img src="/images/visaRequests.png" style="width: 250px;" alt="logo.png">

                                </div>

                      </div>
                      <%--<!-- Page-header end -->--%>
                        <div class="pcoded-inner-content">
                            <!-- Main-body start -->
                            <div class="main-body">
                                <div class="page-wrapper">
                                    <!-- Page-body start -->
                                         <div class="page-body" style="color:black">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <!-- Tab variant tab card start -->
                                                        <div class="card" style="background-image:url('images/bgImage.jpg')">
                                                            <div class="card-header" style="background-color:#09426a">
                                                                <h5 style="color:white">Visa Request - Requirements </h5>
                                                            </div>
                                                            <div class="card-block tab-icon">
                                                                    <div class="col-lg-12 ">
                                                                        <!-- Nav tabs -->
                                                                        <ul class="nav nav-tabs md-tabs " role="tablist">
                                                                         <li class="nav-item">
                                                                                <a class="nav-link active" data-toggle="tab" href="#requestRequirements" role="tab"><i class="icofont icofont-ui-message"></i>Requirements</a>
                                                                                <div class="slide"></div>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#requestDetails" role="tab"><i class="icofont icofont-home"></i>Request Details</a>
                                                                                <div class="slide"></div>
                                                                            </li>
                                                                            <li class="nav-item">
                                                                                <a class="nav-link" data-toggle="tab" href="#managerApproval" role="tab"><i class="icofont icofont-ui-user "></i>Attached Files</a>
                                                                                <div class="slide"></div>
                                                                            </li>
   
                                                                            
                                                                        </ul>
                                                                        <!-- Tab panes -->
                                                                        <div class="tab-content card-block">
                                                                            <div class="tab-pane active" id="requestRequirements" role="tabpanel">
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Visa Requirements</p>
                                                                                </div>
                                                                                    <asp:Label ID="Label14" runat="server" Text="Message:"  Style="margin-left: 30px;font-size:16px"></asp:Label>
                                                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="emailBody" ErrorMessage="*" CssClass="required"></asp:RequiredFieldValidator>
                                                                                <br /> <br />
                                                                                <asp:TextBox runat="server" ID="emailBody" Style="margin-left: 100px;font-size:16px" TextMode="MultiLine" Height="122px" Width="836px"></asp:TextBox>
                                                                                <br /><br /><br />
                                                                                    <asp:Label ID="Label12" runat="server" Text="Attach Files:"  Style="margin-left: 30px;font-size:16px"></asp:Label> <br /> <br />
                                                                                <asp:FileUpload runat="server" ID="requirementFiles" AllowMultiple="true" Style="margin-left: 60px; border-radius: 5px;font-size:16px" />                                              
                                                                                <asp:Button runat="server" class="btn btn-primary" Text="Upload Files" ID="attachFiles" Style="margin-left: 10px; border-radius: 5px;font-size:14px" OnClick="attachFiles_Click" CausesValidation="False"/> <br /><br />
                                                              <div class="card-block">
                                                                    <asp:Label ID="attached" runat="server" style="display:none"> Attachments:</asp:Label> <br />

                                                                <asp:PlaceHolder ID="fileListPlaceholder" runat="server"></asp:PlaceHolder>
                                                                <asp:HiddenField ID="hiddenFilePath" runat="server" />
                                                                <asp:HiddenField ID="hiddenFileLinks" runat="server" />
                                                                  <asp:Button ID="deleteFileButton" runat="server" Text="Delete File" Style="display: none;" OnClick="DeleteFileButton_Click" />
                                                              </div>        <br />
                                                                                <asp:Button runat="server" class="btn btn-primary" Text="Send to Traveller" ID="sendToTraveller" Style="margin-left: 10px; border-radius: 5px;font-size:14px" OnClick="sendToTraveller_Click"/>
                                                                            </div>
                                                                            <div class="tab-pane" id="requestDetails" role="tabpanel">

                                                                                <!--EMPLOYEE DETAILS-->
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Employee Information</p>
                                                                                </div>
                                                                                <div class="card-block">
                                                                                <asp:Label ID="Label11" runat="server" Text="Home Facility"></asp:Label>
                                                                                <asp:TextBox  ID="homeFacility" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                                               
                                                                                    <asp:Label ID="Label1" runat="server" Text="Project Code"  Style="margin-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeProjCode" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                        
                                                                                    </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Employee ID" ></asp:Label>
                                                                                    <asp:TextBox ID="employeeID" runat="server" style="margin-left:20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"  Width="300px"></asp:TextBox> 
                                                               

                                                                                    <asp:Label ID="Label8" runat="server" Text="Level"  style="padding-left:40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeLevel" runat="server"   Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="80px"></asp:TextBox>
                                                                
                                                                                    <asp:Label ID="Label5" runat="server" Text="Department Unit" style="padding-left:50px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeDU" runat="server" Width="200px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                          
                                                                                    </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label7" runat="server" Text="First Name"></asp:Label>
                                                                                    <asp:TextBox ID="employeeFName" runat="server" Width="300px" Style="margin-left: 30px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label19" runat="server" Text="Middle Name" Style="padding-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeMName" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>

                                                                                    <asp:Label ID="Label20" runat="server" Text="Last Name" Style="padding-left: 40px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeLName" runat="server" Width="300px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                                <div class="card-block">

                                                                                    <asp:Label ID="Label3" runat="server" Text="Mobile"></asp:Label>
                                                                                    <asp:TextBox ID="employeePhone" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>
                                                                
                                                                                    <asp:Label ID="Label46" runat="server" Text="Email" Style="padding-left: 45px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeEmail" runat="server" Width="300px" Style="margin-left: 50px; border-radius: 5px" CssClass="textboxes"  Enabled="false" ></asp:TextBox>



                                                                                </div>

                                                                                <!--TRAVEL DETAILS-->
                                                                                <div class="card-block">
                                                                                    <p style="font-size:18px;color:white;background-color:#808080;padding-top:5px;padding-left:5px"> Travel Information</p>
                                                                                </div>
                                                                                <div class="card-block">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Purpose of Travel"></asp:Label>
                                                                                    <asp:TextBox ID="employeePurpose" runat="server" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false" Width="260px" ></asp:TextBox>                                                              
                                                                                     <asp:Label ID="Label10" runat="server" Text="Destination" Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="employeeDestination" runat="server"  Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        
                                                                                    <asp:Label ID="Label4" runat="server" Text="Estimated Travel Date"  Style="padding-left: 50px"></asp:Label>
                                                                                    <asp:TextBox ID="estTravelDate" runat="server"  Width="260px" Style="margin-left: 20px; border-radius: 5px" CssClass="textboxes"  Enabled="false"></asp:TextBox>                                        
                                                            
                                                                                </div>
                                                                                <div class="card-block">

                                                                                </div>
                                                                                <div class="card-block">

                                                                                </div>

                                                                            </div>
                                                                            <div class="tab-pane" id="managerApproval" role="tabpanel">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                <div class="card-block" id="uploadBlock" runat="server">
                                                                                                    <asp:Label ID="Label6" runat="server" Text="Email Approval" Style="margin-left: 20px"></asp:Label> <br /> <br />
                                                                                                    <iframe id="pdfViewer"  runat="server" style="width:100%; height:600px" frameborder="0"></iframe>
                                                                                                </div>   
                                                                                            </div>
                                                                                            <div class="col-lg-12 col-xl-6">
                                                                                                 <div class="card-block" id="Div1" runat="server">
                                                                                                    <asp:Label ID="Label9" runat="server" Text="Scanned Passport" Style="margin-left: 20px"></asp:Label> <br /> <br />
                                                                                                     <iframe id="passportViewer"  runat="server" style="width:100%; height:600px" frameborder="0"></iframe>
                                                                                                </div>   
                                                                                            </div>
                                                                                        </div>

                                                                              
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                          </div>

                                                        </div>
                                                        <!-- Tab variant tab card start -->
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
            </div>
</asp:Content>

