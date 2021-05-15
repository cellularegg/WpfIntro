using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using WpfIntro.DataAccessLayer.Common;
using WpfIntro.DataAccessLayer.DAO;
using WpfIntro.Models;

namespace WpfIntro.DataAccessLayer.PostgressSqlServer
{
    public class MediaItemPostgressDAO : IMediaItemDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"MediaItems\" WHERE \"Id\"=@Id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"MediaItems\";";

        private const string SQL_INSERT_NEW_ITEM =
            "INSERT INTO public.\"MediaItems\" (\"Name\", \"Url\", \"CreationTime\")  VALUES (@Name, @Url, @CreationTime) RETURNING \"Id\";";

        private IDatabase _database;

        public MediaItemPostgressDAO()
        {
            this._database = DALFactory.GetDatabase();
        }
        
        public MediaItemPostgressDAO(IDatabase database)
        {
            _database = database;
        }


        public MediaItem AddNewItem(string name, string url, DateTime creationTime)
        {
            DbCommand insertCommand = _database.CreateCommand(SQL_INSERT_NEW_ITEM);
            _database.DefineParameter(insertCommand, "@Name", DbType.String, name);
            _database.DefineParameter(insertCommand, "@Url", DbType.String, url);
            _database.DefineParameter(insertCommand, "@CreationTime", DbType.String, creationTime.ToString());
            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public MediaItem FindById(int itemId)
        {
            DbCommand findCommand = _database.CreateCommand(SQL_FIND_BY_ID);
            _database.DefineParameter(findCommand, "@Id", DbType.Int32, itemId);
            IEnumerable<MediaItem> mediaItems = QueryMediaItemsFromDatabase(findCommand);
            return mediaItems.FirstOrDefault();
        }

        public IEnumerable<MediaItem> GetItems()
        {
            DbCommand itemsCommand = _database.CreateCommand(SQL_GET_ALL_ITEMS);

            return QueryMediaItemsFromDatabase(itemsCommand);
        }

        private IEnumerable<MediaItem> QueryMediaItemsFromDatabase(DbCommand command)
        {
            List<MediaItem> mediaItemList = new List<MediaItem>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    mediaItemList.Add(new MediaItem(
                        (int) reader["Id"],
                        (string) reader["Name"],
                        (string) reader["Url"],
                        DateTime.Parse(reader["CreationTime"].ToString())
                    ));
                }
            }

            return mediaItemList;
        }
    }
}