using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FlashDrop.Catalog.Application.Abstractions.Services;
using FlashDrop.Catalog.Application.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Catalog.Infrastructure.Services.Storage
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageService(IOptions<CloudinaryOptions> options)
        {
            var account = new Account(
                options.Value.CloudName,
                options.Value.ApiKey,
                options.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImageAsync(FileDto file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Content == null || file.Content.Length == 0)
            {
                throw new ArgumentException("File content cannot be empty.");
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.Content),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Image upload failed: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
