using System.IO;

namespace FlashDrop.Catalog.Application.DTOs
{
    public class FileDto
    {
        public Stream Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public FileDto(Stream content, string fileName, string contentType)
        {
            Content = content;
            FileName = fileName;
            ContentType = contentType;
        }
    }
}
