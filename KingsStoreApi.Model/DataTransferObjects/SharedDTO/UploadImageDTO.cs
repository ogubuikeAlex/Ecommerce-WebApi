using Microsoft.AspNetCore.Http;

namespace KingsStoreApi.Model.DataTransferObjects.SharedDTO
{
    public class UploadImageDTO
    {
        public IFormFile File { get; set; }
        public string Email { get; set; }
    }
}
