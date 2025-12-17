//namespace ProductCatalogService.Features.ProductFeatures.CreateProduct.UploadImage
//{
//    public class ImageHelper : IImageHelper
//    {
//        private readonly IWebHostEnvironment _environment;
//        public ImageHelper(IWebHostEnvironment environment)
//        {
//            _environment = environment;
//        }

//        public async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
//        {
//            if (imageFile == null || imageFile.Length == 0)
//                throw new ArgumentException("Invalid image file");
//            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", folderName);
//            Directory.CreateDirectory(uploadsFolder);

//            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
//            var filePath = Path.Combine(uploadsFolder, fileName);

//            using var stream = new FileStream(filePath, FileMode.Create);
//            await imageFile.CopyToAsync(stream);
//            return $"/images/{folderName}/{fileName}";
//        }
//    }
//}
