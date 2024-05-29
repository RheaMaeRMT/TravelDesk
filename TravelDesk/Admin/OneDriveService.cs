using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TravelDesk.Admin
{
    public class OneDriveService
    {
        private readonly string clientId = "YOUR_CLIENT_ID";
        private readonly string tenantId = "YOUR_TENANT_ID";
        private readonly string clientSecret = "YOUR_CLIENT_SECRET";
        private readonly string[] scopes = { "https://graph.microsoft.com/.default" };

        private GraphServiceClient GetGraphServiceClient()
        {
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            return new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                var authResult = await confidentialClientApplication
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                requestMessage.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            }));
        }

        public async Task<string> UploadFileToOneDriveAsync(string filePath)
        {
            var graphClient = GetGraphServiceClient();

            // Upload file
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var uploadedItem = await graphClient.Me.Drive.Root
                    .ItemWithPath(Path.GetFileName(filePath))
                    .Content
                    .Request()
                    .PutAsync<DriveItem>(fileStream);

                // Get the shareable link
                var permission = await graphClient.Me.Drive.Items[uploadedItem.Id]
                    .CreateLink("view")
                    .Request()
                    .PostAsync();

                return permission.Link.WebUrl;
            }
        }
    }
}