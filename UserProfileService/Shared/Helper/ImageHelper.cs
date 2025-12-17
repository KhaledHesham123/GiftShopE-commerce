
using UserProfileService.Shared.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UserProfileService.Shared.Helper;

public class ImageHelper(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IImageHelper
{
    
    async Task<string> IImageHelper.SaveImageAsync(IFormFile imageFile, string subFolder)
    {
        if (imageFile == null || imageFile.Length == 0)
            throw new ArgumentException("Image file is required.");

        //fileImage
        var fileExtention = Path.GetExtension(imageFile.FileName); // return name "Image.type"                                                                    
        var fileName = $"{Guid.NewGuid()}{imageFile.FileName}";
        //get filepath
        var webRootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" ,"Images", subFolder);

        var folderPath = Path.Combine(webRootPath, "Images", subFolder);

        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return Path.Combine("Uploads", "Images", subFolder, fileName).Replace("\\", "/"); 
    }

    string IImageHelper.GetImageUrl(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return null;


        if (Uri.IsWellFormedUriString(relativePath, UriKind.Absolute))
            return relativePath;

        relativePath = relativePath.Replace("\\", "/").TrimStart('/');

        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null)
            return "/" + relativePath;

        var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
        return $"{baseUrl}/{relativePath}";
    }

    public bool DeleteImage(string relativePath, string subFolder )
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return false;

        var webRootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        relativePath = relativePath.Replace("\\", "/").TrimStart('/');
        var fullPath = Path.Combine(webRootPath, relativePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }

        return false;
    }
     
}
