using System;
using System.Collections.Generic;
using WpfIntro.Models;

namespace WpfIntro.DataAccessLayer.DAO
{
    public interface IMediaItemDAO
    {
        MediaItem FindById(int itemId);
        MediaItem AddNewItem(string name, string url, DateTime creationTime);
        IEnumerable<MediaItem> GetItems();
        // TODO UPDATE and DELETE
    }
}