namespace Project.Domain.Extensions
{
    public static class IconFileExtension
    {
        public static FileType GetFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !fileName.Contains('.'))
                return FileType.Unknown;

            // Lấy phần đuôi tệp (file extension)
            var tailFile = fileName.Split('.').Last().ToLower();

            // Ánh xạ phần đuôi tệp tới enum FileType
            return tailFile switch
            {
                "txt" => FileType.Txt,
                "doc" => FileType.Doc,
                "docx" => FileType.Docx,
                "pdf" => FileType.Pdf,
                "odt" => FileType.Odt,
                "rtf" => FileType.Rtf,

                "jpg" or "jpeg" => FileType.Jpg,
                "png" => FileType.Png,
                "gif" => FileType.Gif,
                "bmp" => FileType.Bmp,
                "svg" => FileType.Svg,
                "tiff" or "tif" => FileType.Tiff,

                "mp3" => FileType.Mp3,
                "wav" => FileType.Wav,
                "flac" => FileType.Flac,
                "aac" => FileType.Aac,
                "ogg" => FileType.Ogg,

                "mp4" => FileType.Mp4,
                "avi" => FileType.Avi,
                "mkv" => FileType.Mkv,
                "mov" => FileType.Mov,
                "wmv" => FileType.Wmv,
                "flv" => FileType.Flv,

                "zip" => FileType.Zip,
                "rar" => FileType.Rar,
                "7z" => FileType.SevenZip,
                "tar" => FileType.Tar,
                "gz" => FileType.Gz,
                "iso" => FileType.Iso,

                "c" => FileType.C,
                "cpp" => FileType.Cpp,
                "cs" => FileType.Cs,
                "java" => FileType.Java,
                "py" => FileType.Py,
                "js" => FileType.Js,
                "ts" => FileType.Ts,
                "html" => FileType.Html,
                "css" => FileType.Css,

                "sql" => FileType.Sql,
                "db" => FileType.Db,
                "sqlite" => FileType.Sqlite,
                "mdb" => FileType.Mdb,
                "csv" => FileType.Csv,

                "exe" => FileType.Exe,
                "dll" => FileType.Dll,
                "bat" => FileType.Bat,
                "sh" => FileType.Sh,
                "app" => FileType.App,

                "sys" => FileType.Sys,
                "ini" => FileType.Ini,
                "log" => FileType.Log,
                "cfg" => FileType.Cfg,

                "psd" => FileType.Psd,
                "ai" => FileType.Ai,
                "xd" => FileType.Xd,
                "sketch" => FileType.Sketch,
                "dwg" => FileType.Dwg,

                _ => FileType.Unknown, // Nếu không có trong danh sách
            };
        }
    }
}
