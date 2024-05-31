using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.IO;

public class GoogleDriveService
{
    private readonly DriveService _service;

    public GoogleDriveService()
    {
        // Load the client secrets JSON file (from Google Cloud Console)
        GoogleCredential credential;
        using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.ScopeConstants.DriveFile);
        }

        // Create Drive API service
        _service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Your Application Name",
        });
    }

    public string UploadFile(string filePath)
    {
        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = Path.GetFileName(filePath),
        };

        FilesResource.CreateMediaUpload request;
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            request = _service.Files.Create(fileMetadata, stream, "application/pdf");
            request.Fields = "id, webViewLink";
            request.Upload();
        }

        var file = request.ResponseBody;
        return file.WebViewLink; // Return the Google Drive file URL
    }
}
