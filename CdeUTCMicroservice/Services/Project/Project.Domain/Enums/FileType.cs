namespace Project.Domain.Enums
{
    public enum FileType
    {
        // Văn bản
        Txt,        // .txt
        Doc,        // .doc
        Docx,       // .docx
        Pdf,        // .pdf
        Odt,        // .odt
        Rtf,        // .rtf

        // Hình ảnh
        Jpg,        // .jpg / .jpeg
        Png,        // .png
        Gif,        // .gif
        Bmp,        // .bmp
        Svg,        // .svg
        Tiff,       // .tiff / .tif

        // Âm thanh
        Mp3,        // .mp3
        Wav,        // .wav
        Flac,       // .flac
        Aac,        // .aac
        Ogg,        // .ogg

        // Video
        Mp4,        // .mp4
        Avi,        // .avi
        Mkv,        // .mkv
        Mov,        // .mov
        Wmv,        // .wmv
        Flv,        // .flv

        // Tệp nén
        Zip,        // .zip
        Rar,        // .rar
        SevenZip,   // .7z
        Tar,        // .tar
        Gz,         // .gz
        Iso,        // .iso

        // Mã nguồn
        C,          // .c
        Cpp,        // .cpp
        Cs,         // .cs
        Java,       // .java
        Py,         // .py
        Js,         // .js
        Ts,         // .ts
        Html,       // .html
        Css,        // .css

        // Cơ sở dữ liệu
        Sql,        // .sql
        Db,         // .db
        Sqlite,     // .sqlite
        Mdb,        // .mdb
        Csv,        // .csv

        // Tệp thực thi
        Exe,        // .exe
        Dll,        // .dll
        Bat,        // .bat
        Sh,         // .sh
        App,        // .app

        // Hệ thống
        Sys,        // .sys
        Ini,        // .ini
        Log,        // .log
        Cfg,        // .cfg

        // Đồ họa
        Psd,        // .psd
        Ai,         // .ai
        Xd,         // .xd
        Sketch,     // .sketch
        Dwg   ,      // .dwg
        Unknown
    }

}
