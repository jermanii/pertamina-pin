using Pertamina.IRIS.Utility.MimeType.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pertamina.IRIS.Utility.MimeType.Implement
{
    public class MimeTypeUtility : IMimeTypeUtility
    {
        public MimeTypeUtility()
        {

        }

        public string GetMimeType(string fileExtension)
        {
            var mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".txt", "text/plain" },
                { ".html", "text/html" },
                { ".htm", "text/html" },
                { ".css", "text/css" },
                { ".js", "application/javascript" },
                { ".json", "application/json" },
                { ".xml", "application/xml" },
                { ".csv", "text/csv" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".bmp", "image/bmp" },
                { ".webp", "image/webp" },
                { ".svg", "image/svg+xml" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/msword" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".ppt", "application/vnd.ms-powerpoint" },
                { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
                { ".zip", "application/zip" },
                { ".rar", "application/vnd.rar" },
                { ".7z", "application/x-7z-compressed" },
                { ".tar", "application/x-tar" },
                { ".gz", "application/gzip" },
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".ogg", "audio/ogg" },
                { ".mp4", "video/mp4" },
                { ".mkv", "video/x-matroska" },
                { ".avi", "video/x-msvideo" },
                { ".mov", "video/quicktime" },
                { ".flv", "video/x-flv" },
                { ".wmv", "video/x-ms-wmv" },
                { ".exe", "application/octet-stream" },
                { ".bin", "application/octet-stream" },
                { ".iso", "application/x-iso9660-image" }
            };

            // Return the MIME type or default to "application/octet-stream"
            return mimeTypes.TryGetValue(fileExtension, out var mimeType) ? mimeType : "application/octet-stream";
        }
    }
}
