using System.Data.Common;
using System.Data;

namespace CactiService.Repos
{
    public class RepoUtils
    {
        public static async Task<DbConnection> GetConnection(IDatabase db)
        {
            var connection = db.GetConnection();
            await connection.OpenAsync();
            return connection;
        }

        public static DbCommand CreateCommand(DbConnection conn, string sql)
        {
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static async Task ExecuteNonQuery(DbConnection conn, string sql, Action<DbCommand> addParameters, Action<DbCommand>? beforeExecute = null)
        {
            using DbCommand cmd = CreateCommand(conn, sql);
            addParameters(cmd);

            beforeExecute?.Invoke(cmd);

            await cmd.ExecuteNonQueryAsync();
        }

    }
}
