using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Azure.Core;
using ImportExportFile.Application.Abstractions.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace ImportExportFile.Infrastructure.Files.Storages
{
    public class S3FileStorage : IFileStorage
    {
        private readonly ILogger<S3FileStorage> _logger;
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;

        public S3FileStorage(IOptions<AwsSettings> awsOptions, ILogger<S3FileStorage> logger, IAmazonS3 s3Client)
        {
            var awsSettings = awsOptions.Value;
            _logger = logger;
            _bucketName = awsSettings.BucketName;
            _s3Client = s3Client;

        }

        public async Task<string> SaveAsync(IFormFile file, string? folder = null)
        {
            try
            {

                var fileKey = FileHelper.BuildS3Key(folder);

                using var stream = file.OpenReadStream();
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = stream,
                    Key = fileKey,
                    BucketName = _bucketName,
                    ContentType = file.ContentType,
                    Metadata = { ["file-name"] = Uri.EscapeDataString(file.FileName) }
                };

                var transferUtility = new TransferUtility(_s3Client);
                await transferUtility.UploadAsync(uploadRequest);

                _logger.LogInformation("Uploaded file to S3: {Key}", fileKey);


                return fileKey;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to S3");
                throw;
            }
        }

       
    }
}
