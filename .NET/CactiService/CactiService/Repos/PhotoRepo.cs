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
            obj.Id = reader.RGetLong("id");
            obj.Code = reader.RGetString("code");
            obj.Path = reader.RGetString("path");
            obj.LastModified = GRPCUtils.ConvertDate(reader.RGetDateTime("lastModified"));
        }

        protected override void AddSqlParameters(DbCommand cmd, Photo obj)
        {
            cmd.AddDbValue("id", obj.Id);
            cmd.AddDbValue("code", obj.Code);
            cmd.AddDbValue("path", obj.Path);
            cmd.AddDbValue("lastModified", GRPCUtils.ConvertDate(obj.LastModified));
        }

        protected override void SetId(Photo obj, long id) => obj.Id = id;
        protected override long GetId(Photo obj) => obj.Id;

    }
}
