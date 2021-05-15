using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WpfIntro.DataAccessLayer;
using WpfIntro.DataAccessLayer.Common;
using WpfIntro.DataAccessLayer.DAO;
using WpfIntro.Models;

namespace WpfIntro.BusinessLayer
{
    internal class WpfIntroFactoryImpl : IWpfIntroFactory
    {
        public IEnumerable<MediaItem> GetItems()
        {
            IMediaItemDAO mediaItemDAO = DALFactory.CreateMediaItemDAO();
            return mediaItemDAO.GetItems();
        }

        public IEnumerable<MediaItem> Search(string itemName, bool caseSensitive = false)
        {
            IEnumerable<MediaItem> items = GetItems();
            if (caseSensitive)
            {
                return items.Where(i => i.Name.Contains(itemName));
            }

            return items.Where(i => i.Name.ToLower().Contains(itemName.ToLower()));
        }

        public MediaItem CreateItem(string name, string url, DateTime creationTime)
        {
            IMediaItemDAO mediaItemDAO = DALFactory.CreateMediaItemDAO();
            return mediaItemDAO.AddNewItem(name, url, creationTime);
        }

        public MediaLog CreateItemLog(string logText, MediaItem item)
        {
            IMediaLogDAO mediaLogDAO = DALFactory.CreateMediaLogDAO();
            return mediaLogDAO.AddNewItemLog(logText, item);
        }
    }
}