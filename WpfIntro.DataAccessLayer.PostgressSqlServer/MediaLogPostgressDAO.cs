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
    public class MediaLogPostgressDAO : IMediaLogDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"MediaLogs\" WHERE \"Id\"=@Id;";

        private const string SQL_FIND_BY_MEDIA_ITEM =
            "SELECT * FROM public.\"MediaLogs\" WHERE \"MediaItemId\"=@MediaItemId;";

        private const string SQL_INSERT_NEW_ITEMLOG =
            "INSERT INTO public.\"MediaLogs\" (\"LogText\", \"MediaItemId\") VALUES (@LogText, @MediaItemId);";

        private IDatabase _database;
        private IMediaItemDAO _mediaItemDAO;

        public MediaLogPostgressDAO()
        {
            _database = DALFactory.GetDatabase();
            _mediaItemDAO = DALFactory.CreateMediaItemDAO();
        }
        
        public MediaLogPostgressDAO(IDatabase database, IMediaItemDAO mediaItemDao)
        {
            _database = database;
            _mediaItemDAO = mediaItemDao;
        }

        public MediaLog AddNewItemLog(string logText, MediaItem item)
        {
            DbCommand insertCommand = _database.CreateCommand(SQL_INSERT_NEW_ITEMLOG);
            _database.DefineParameter(insertCommand, "@LogText", DbType.String, logText);
            _database.DefineParameter(insertCommand, "@MediaItemId", DbType.Int32, item.Id);

            return FindById(_database.ExecuteScalar(insertCommand));
        }

        public MediaLog FindById(int logId)
        {
            DbCommand findCommand = _database.CreateCommand(SQL_FIND_BY_ID);
            _database.DefineParameter(findCommand, "@Id", DbType.Int32, logId);
            IEnumerable<MediaLog> mediaLogs = QueryMediaLogsFromDb(findCommand);
            return mediaLogs.FirstOrDefault();
        }

        public IEnumerable<MediaLog> GetLogsForMediaItem(MediaItem item)
        {
            DbCommand getLogsCommand = _database.CreateCommand(SQL_FIND_BY_MEDIA_ITEM);
            _database.DefineParameter(getLogsCommand, "@MediaItemId", DbType.Int32, item.Id);
            IEnumerable<MediaLog> mediaLogs = QueryMediaLogsFromDb(getLogsCommand);
            return mediaLogs;
        }

        private IEnumerable<MediaLog> QueryMediaLogsFromDb(DbCommand command)
        {
            List<MediaLog> mediaLogList = new List<MediaLog>();

            using (IDataReader reader = _database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    mediaLogList.Add(new MediaLog(
                        (int) reader["Id"],
                        (string) reader["LogText"],
                        _mediaItemDAO.FindById((int) reader["MediaItemId"])
                    ));
                }
            }

            return mediaLogList;
        }
    }
}