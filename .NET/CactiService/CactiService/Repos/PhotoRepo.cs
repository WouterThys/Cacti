using CactiServer.Managers;
using Common.Proto;
using Database;
using Shared.Utils;
using System.Data.Common;

namespace CactiServer.Repos
{
    public class PhotoRepo : BaseRepo<Photo>
    {
        public PhotoRepo(ILogger<PhotoRepo> logger, IDatabase db, ICallbackManager callback) : base("photos", logger, db, callback)
        { 
        }


        protected override void InitializeFromReader(DbDataReader reader, DbConnection conn, Photo obj) 
        {
            obj.Id = DatabaseAccess.RGetLong(reader, "id");
            obj.Code = DatabaseAccess.RGetString(reader, "code");
            obj.Path = DatabaseAccess.RGetString(reader, "path");
            obj.LastModified = GRPCUtils.ConvertDate(DatabaseAccess.RGetDateTime(reader, "lastModified"));
        }

        protected override void AddSqlParameters(DbCommand cmd, Photo obj)
        {
            DatabaseAccess.AddDbValue(cmd, "id", obj.Id);
            DatabaseAccess.AddDbValue(cmd, "code", obj.Code);
            DatabaseAccess.AddDbValue(cmd, "path", obj.Path);
            DatabaseAccess.AddDbValue(cmd, "lastModified", GRPCUtils.ConvertDate(obj.LastModified));
        }

        protected override void SetId(Photo obj, long id) => obj.Id = id;
        protected override long GetId(Photo obj) => obj.Id;

    }
}
