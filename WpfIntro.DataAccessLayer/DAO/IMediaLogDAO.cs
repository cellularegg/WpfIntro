using System.Collections.Generic;
using WpfIntro.Models;

namespace WpfIntro.DataAccessLayer.DAO
{
    public interface IMediaLogDAO
    {
        MediaLog FindById(int logId);
        MediaLog AddNewItemLog(string logText, MediaItem item);
        IEnumerable<MediaLog> GetLogsForMediaItem(MediaItem item);

        // TODO UPDATE and DELETE
    }
}