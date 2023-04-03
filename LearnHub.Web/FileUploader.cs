using LearnHub.Application.Common.Interfaces;

namespace LearnHub.Web
{
  
    public class FileUploader:IFileUploader
    {
        private readonly IWebHostEnvironment
             _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public FileUploadResponse Upload(IFormFile file,params string[] paths)
        {
            if(file == null)
                return null;

            var pathInput = String.Join('\\',paths);
            
            var directoryPath = Path.Join(_webHostEnvironment.WebRootPath, "Files",pathInput);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = DateTime.Now.ToFileTime() + "-" + file.FileName;

            var filePath = Path.Combine(directoryPath, fileName);

            using var output = File.Create(filePath);
            file.CopyTo(output);
            var result = new FileUploadResponse()
            {
                Path = $"Files/{String.Join('/', paths)}/{fileName}",
                FileName = fileName
            };
            return result;
        }

        public void RemoveFile(string pathFile)
        {
            if (!string.IsNullOrWhiteSpace(pathFile))
            {
                pathFile = pathFile.Replace("/", "\\");
                var pathFilePhysics = Path.Combine(_webHostEnvironment.WebRootPath, "Files", pathFile);
                if (File.Exists(pathFilePhysics))
                    File.Delete(pathFilePhysics);
            }
      
        }
    }
}
