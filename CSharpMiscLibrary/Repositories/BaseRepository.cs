using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CSharpMiscLibrary.Repositories
{
    /// <summary>
    /// Database connector using Dapper ORM.
    /// </summary>
    public class BaseRepository
    {
        private IDbConnection Connection = null;
        /// <summary>
        /// SQL String connection to instantiate a connection.
        /// </summary>
        public string SqlStringConnection { get; set; } = null;

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public BaseRepository() { }

        /// <summary>
        /// Default constructor. Gets SQL string connection, instantiate and stores the new connection.
        /// </summary>
        /// <param name="SqlStringConnection">Full string SQL connection</param>
        public BaseRepository(string SqlStringConnection)
        {
            if (SqlStringConnection.Equals(null)) this.SqlStringConnection = SqlStringConnection;
            GetConnection();
        }

        /// <summary>
        /// Get DB connection and instantiates a new one if doesn't exist.
        /// </summary>
        /// <returns>IDbConnection.</returns>
        private IDbConnection GetConnection()
        {
            if (this.Connection == null) this.Connection = new SqlConnection(this.SqlStringConnection);
            return this.Connection;
        }

        /// <summary>
        /// Middleware function to insert a new row into table.
        /// </summary>
        /// <typeparam name="T">Dynamic type of used class</typeparam>
        /// <param name="sql">Manually written SQL string</param>
        /// <param name="parameters">Array of paramaters' objects.</param>
        /// <returns>Quantity of affected rows.</returns>
        protected int InsertIntoTable<T>(string sql, object parameters = null)
        {
            return GetConnection().Execute(sql, parameters);
        }

        /// <summary>
        /// Middleware function to query first/default row from table.
        /// </summary>
        /// <typeparam name="T">Dynamic type of used class</typeparam>
        /// <param name="sql">Manually written SQL string</param>
        /// <param name="parameters">Array of paramaters' objects.</param>
        /// <returns>Object from same dynamic class type.</returns>
        protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            return GetConnection().QueryFirstOrDefault<T>(sql, parameters);
        }

        /// <summary>
        /// Middleware function to query a list of rows from table.
        /// </summary>
        /// <typeparam name="T">Dynamic type of used class</typeparam>
        /// <param name="sql">Manually written SQL string</param>
        /// <param name="parameters">Array of paramaters' objects.</param>
        /// <returns>Object list from same dynamic class type.</returns>
        protected List<T> Query<T>(string sql, object parameters = null)
        {
            return GetConnection().Query<T>(sql, parameters).ToList();
        }

        /// <summary>
        /// Middleware function to execute a custom SQL query.
        /// </summary>
        /// <param name="sql">Manually written SQL string</param>
        /// <param name="parameters">Array of paramaters' objects.</param>
        /// <returns>Quantity of affected rows.</returns>
        protected int Execute(string sql, object parameters = null)
        {
            return GetConnection().Execute(sql, parameters);
        }
    }
}
