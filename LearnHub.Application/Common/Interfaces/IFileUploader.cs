using Microsoft.AspNetCore.Http;

namespace LearnHub.Application.Common.Interfaces
{
    public class FileUploadResponse
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
    public interface IFileUploader
    {
        FileUploadResponse Upload(IFormFile file,params string[] paths);

        void RemoveFile(string pathFile);
    }
}
