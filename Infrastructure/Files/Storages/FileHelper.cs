using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ImportExportFile.Infrastructure.Files.Storages
{
    public static class FileHelper
    {
        public static string BuildS3Key(string? folder = null)
        {
            if (string.IsNullOrWhiteSpace(folder))
                return $"uploads/{Guid.NewGuid()}";

            return $"{folder.Trim().TrimStart('/').TrimEnd('/')}/{Guid.NewGuid()}";
        }
    }
}
