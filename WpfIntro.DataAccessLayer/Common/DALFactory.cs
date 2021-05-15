using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using WpfIntro.DataAccessLayer.DAO;

namespace WpfIntro.DataAccessLayer.Common
{
    public class DALFactory
    {
        private static string _assemblyName;
        private static Assembly _dalAssembly;
        private static IDatabase _database;

        static DALFactory()
        {
            _assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            Debug.WriteLine(ConfigurationManager.AppSettings["DALSqlAssembly"]);
            _dalAssembly = Assembly.Load(_assemblyName);
        }

        public static IDatabase GetDatabase()
        {
            if (_database == null)
            {
                _database = CreateDatabase();
            }

            return _database;
        }

        private static IDatabase CreateDatabase()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["PostgressSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        private static IDatabase CreateDatabase(string connectionString)
        {
            string dataBaseClassName = _assemblyName + ".Database";
            Type dbClass = _dalAssembly.GetType(dataBaseClassName);
            return Activator.CreateInstance(dbClass, new object[] {connectionString}) as IDatabase;
        }

        public static IMediaItemDAO CreateMediaItemDAO()
        {
            string className = _assemblyName + ".MediaItemPostgressDAO";
            Type mediaItemType = _dalAssembly.GetType(className);
            return Activator.CreateInstance(mediaItemType) as IMediaItemDAO;
        }

        public static IMediaLogDAO CreateMediaLogDAO()
        {
            string className = _assemblyName + ".MediaLogPostgressDAO";
            Type mediaLogType = _dalAssembly.GetType(className);
            return Activator.CreateInstance(mediaLogType) as IMediaLogDAO;
        }
    }
}