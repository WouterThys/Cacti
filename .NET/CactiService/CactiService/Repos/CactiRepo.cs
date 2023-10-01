using Common.Proto;
using Common.Services.Cactuses;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using static CactiService.Repos.RepoUtils;

namespace CactiService.Repos
{
    public class CactiRepo
    {
        private const string TableName = "cacti";
        private const string SqlSelectAll = TableName + "SelectAll";
        private const string SqlInsert = TableName + "Insert";
        private const string SqlUpdate = TableName + "Update";
        private const string SqlDelete = TableName + "Delete";

        protected readonly ILogger _logger;
        protected readonly IDatabase _db;



        private static void InitializeBaseFromReader(DbDataReader reader, DbConnection conn, Cactus cactus) 
        {
        }

        private static void AddSqlParameters(DbCommand cmd, Document obj)
        {
            
        }


        public async Task<IEnumerable<Cactus>> GetBy(IEnumerable<long> ids) 
        {
            _logger.LogTrace("Fetching data for {Table}, sql={Sql}", TableName, SqlSelectAll);

            var list = new List<Cactus>();

            await using (var conn = await GetConnection(_db))
            {

                // Select objects
                using DbCommand cmd = CreateCommand(conn, SqlSelectAll);
                using DbDataReader reader = cmd.ExecuteReader();

                while (await reader.ReadAsync())
                {
                    var cactus = new Cactus();
                    InitializeBaseFromReader(reader, conn, cactus);
                    list.Add(cactus);
                }
            }

            _logger.LogTrace("Loaded {} items for {Table}", list.Count, TableName);

            return list;
        }

        public async Task<Document> Save(Document obj)
        {
            if (obj.Id <= 0)
            {
                return await Insert(obj);
            }
            else
            {
                return await Update(obj);
            }
        }

        private async Task<Cactus> Insert(Cactus obj)
        {
            DbParameter lastInsertId = new MySql.Data.MySqlClient.MySqlParameter
            {
                ParameterName = "@lid", // Last insert id
                Direction = ParameterDirection.Output
            };

            await using (var conn = await GetConnection(_db))
            {
                using var transation = conn.BeginTransaction();

                await ExecuteNonQuery(conn, SqlInsert,
                    (cmd) => AddSqlParameters(cmd, obj),
                    (cmd) => cmd.Parameters.Add(lastInsertId)
                );

                // Update id
                obj.Id = Convert.ToInt64(lastInsertId.Value);

                transation.Commit();
            }

            OnChanged(DbActionType.Insert, obj);

            return obj;
        }

        public virtual async Task<Cactus> Update(Cactus obj)
        {

            await using (var conn = await GetConnection(_db))
            {
                using var transation = conn.BeginTransaction();
                await ExecuteNonQuery(conn, SqlUpdate, (cmd) =>
                {
                    AddSqlParameters(cmd, obj);
                    DatabaseAccess.AddDbValue(cmd, "updateId", obj.Id);
                });

                transation.Commit();
            }

            OnChanged(DbActionType.Update, obj);

            return obj;
        }

        public async Task<long> Delete(long id)
        {
            var dummy = new Document() { Id = id };
            if (Constants.IsValidId(id))
            {
                await using (var conn = await GetConnection(_db))
                {
                    using var transation = conn.BeginTransaction();

                    await ExecuteNonQuery(conn, SqlDelete, (cmd) =>
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

        private void OnChanged(DbActionType type, Cactus? obj)
        {
            _logger.LogTrace("Object changed: {Code} {Type}", obj?.Code, type);

            if (obj != null)
            {
                _callback.OnChanged(type, obj);
            }
        }
    }
}
