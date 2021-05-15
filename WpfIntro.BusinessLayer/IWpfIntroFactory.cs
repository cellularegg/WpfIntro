using System;
using System.Collections;
using System.Collections.Generic;
using WpfIntro.Models;

namespace WpfIntro.BusinessLayer
{
    public interface IWpfIntroFactory
    {
        IEnumerable<MediaItem> GetItems();
        IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false);
        MediaItem CreateItem(string name, string url, DateTime creationTime);
        MediaLog CreateItemLog(string logText, MediaItem item);
    }
}