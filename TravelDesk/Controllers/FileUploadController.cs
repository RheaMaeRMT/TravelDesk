
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Save the file temporarily
        var filePath = Path.GetTempFileName();
        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        // Upload file to Google Drive
        var googleDriveService = new GoogleDriveService();
        var fileLink = googleDriveService.UploadFile(filePath);

        // Delete the temporary file
        System.IO.File.Delete(filePath);

        return Ok(new { link = fileLink });
    }
}
