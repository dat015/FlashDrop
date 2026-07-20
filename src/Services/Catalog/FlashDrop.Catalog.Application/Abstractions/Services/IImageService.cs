using FlashDrop.Catalog.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Application.Abstractions.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(FileDto file, CancellationToken cancellationToken = default);
    }
}
