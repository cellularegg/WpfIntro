using System;
using System.Collections.Generic;
using System.IO;
using WpfIntro.Models;

namespace WpfIntro.DataAccessLayer.Common
{
    public interface IFileAccess
    {
        int CreateNewMediaItemFile(string name, string url, DateTime createTime);
        int CreateNewMediaLogFile(string logText, MediaItem logMediaItem);
        IEnumerable<FileInfo> SearchFiles(string searchTerm, MediaTypes searchType);
        IEnumerable<FileInfo> GetAllFiles(MediaTypes searchType);
    }
}