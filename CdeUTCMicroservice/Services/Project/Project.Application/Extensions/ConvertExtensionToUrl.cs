namespace Project.Application.Extensions
{
    public static class ConvertExtensionToUrl
    {
        private static readonly Dictionary<string, string> ExtensionToImageMap = new()
        {
            { ".pdf", "pdf.jpg" },
            { ".doc", "docx.png" },
            { ".docx", "docx.png" },
            { ".xls", "xls.png" },
            { ".xlsx", "xls.png" },
            { ".ppt", "ppt.png" },
            { ".pptx", "ppt.png" },
            { ".txt", "txt.png" },
            { ".zip", "zip.png" },
            { ".rar", "rar.png" },
            { ".gif", "image.gif" },
            { ".mp3", "audio.png" },
            { ".mp4", "video.png" },
            { ".extension", "file.png" }
        };

        public static string ConvertToUrl(this string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
                return "unknown.png"; // Ảnh mặc định nếu không xác định được

            extension = extension.ToLowerInvariant().Trim();

            if (!extension.StartsWith("."))
                extension = "." + extension; // Đảm bảo luôn có dấu chấm

            return ExtensionToImageMap.TryGetValue(extension, out var url) ? url : "unknown.png";
        }
    }
}
