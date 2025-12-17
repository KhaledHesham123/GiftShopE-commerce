namespace UserProfileService.Shared.Interfaces
{
    public interface IImageHelper
    {
      public Task<string> SaveImageAsync(IFormFile imageFile, string subFolder);
      public string GetImageUrl(string relativePath);
      public bool DeleteImage(string relativePath, string subFolder);
    }
}
