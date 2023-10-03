using CactiServer.Managers;
using Database;
using System.Data.Common;
using System.Data;

namespace CactiServer.Repos
{
    public abstract class BaseRepo<T> where T : class, new()
    {
        protected readonly string TableName;
        protected readonly string SqlSelectAll;
        protected readonly string SqlSelectById;
        protected readonly string SqlInsert;
        protected readonly string SqlUpdate;
        protected readonly string SqlDelete;

        protected readonly ILogger _logger;
        protected readonly IDatabase _db;
        protected readonly ICallbackManager _callback;


        protected abstract void SetId(T obj, long id);
        protected abstract long GetId(T obj);


        public BaseRepo(string tableName, ILogger logger, IDatabase db, ICallbackManager callback)
        {
            TableName = tableName;
            SqlSelectAll = TableName + "SelectAll";
            SqlSelectById = TableName + "ById";
            SqlInsert = TableName + "Insert";
            SqlUpdate = TableName + "Update";
            SqlDelete = TableName + "Delete";

            _db = db;
            _logger = logger;
            _callback = callback;
        }


        protected abstract void InitializeFromReader(DbDataReader reader, DbConnection conn, T obj);

        protected abstract void AddSqlParameters(DbCommand cmd, T obj);


        public async Task<IEnumerable<T>> GetBy(IEnumerable<long> ids)
        {
            string sql;
            if (ids != null && ids.Any()) 
            {
                sql = SqlSelectById;
            }
            else
            {
                sql = SqlSelectAll;
            }

            _logger.LogTrace("Fetching data for {Table}, sql={Sql}", TableName, sql);

            var list = new List<T>();

            await using (var conn = await RepoUtils.GetConnection(_db))
            {
                // Select objects
                using DbCommand cmd = RepoUtils.CreateCommand(conn, sql);

                if (ids != null && ids.Any()) 
                {
                    DatabaseAccess.AddDbValue(cmd, "id", ids.FirstOrDefault());
                }

                using DbDataReader reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    var t = new T();
                    InitializeFromReader(reader, conn, t);
                    list.Add(t);
                }
            }

            _logger.LogTrace("Loaded {} items for {Table}", list.Count, TableName);

            return list;
        }

        public async Task<T> Save(T obj)
        {
            if (GetId(obj) <= 0)
            {
                return await Insert(obj);
            }
            else
            {
                return await Update(obj);
            }
        }

        private async Task<T> Insert(T obj)
        {
            DbParameter lastInsertId = new MySql.Data.MySqlClient.MySqlParameter
            {
                ParameterName = "@lid", // Last insert id
                Direction = ParameterDirection.Output
            };

            await using (var conn = await RepoUtils.GetConnection(_db))
            {
                using var transation = conn.BeginTransaction();

                await RepoUtils.ExecuteNonQuery(conn, SqlInsert,
                    (cmd) => AddSqlParameters(cmd, obj),
                    (cmd) => cmd.Parameters.Add(lastInsertId)
                );

                // Update id
                SetId(obj, Convert.ToInt64(lastInsertId.Value));

                transation.Commit();
            }

            OnChanged(DbActionType.Insert, obj);

            return obj;
        }

        public virtual async Task<T> Update(T obj)
        {

            await using (var conn = await RepoUtils.GetConnection(_db))
            {
                using var transation = conn.BeginTransaction();
                await RepoUtils.ExecuteNonQuery(conn, SqlUpdate, (cmd) =>
                {
                    AddSqlParameters(cmd, obj);
                    DatabaseAccess.AddDbValue(cmd, "updateId", GetId(obj));
                });

                transation.Commit();
            }

            OnChanged(DbActionType.Update, obj);

            return obj;
        }

        public async Task<long> Delete(long id)
        {
            var dummy = new T();
            SetId(dummy, id);

            if (id > 1)
            {
                await using (var conn = await RepoUtils.GetConnection(_db))
                {
                    using var transation = conn.BeginTransaction();

                    await RepoUtils.ExecuteNonQuery(conn, SqlDelete, (cmd) =>
                    {
                        DatabaseAccess.AddDbValue(cmd, "deleteId", id);
                    });

                    transation.Commit();
                }

                OnChanged(DbActionType.Delete, dummy);
            }
            else
            {
                _logger.LogWarning("Unable to delete document with id {}", id);
            }
            return id;
        }

        private void OnChanged(DbActionType type, T? obj)
        {
            _logger.LogTrace("Object changed: {Code} {Type}", typeof(T).Name, type);

            if (obj != null)
            {
                _callback.OnChanged(type, obj);
            }
        }
    }
}
