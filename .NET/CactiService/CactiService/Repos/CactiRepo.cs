using CactiServer.Managers;
using Common.Proto;
using Database;
using Shared.Utils;
using System.Data.Common;

namespace CactiServer.Repos
{
    public class CactiRepo : BaseRepo<Cactus>
    {

        public CactiRepo(ILogger<CactiRepo> logger, IDatabase db, ICallbackManager callback) : base("cacti", logger, db, callback)
        { 

        }


        protected override void InitializeFromReader(DbDataReader reader, DbConnection conn, Cactus obj) 
        {
            obj.Id = DatabaseAccess.RGetLong(reader, "id");
            obj.Code = DatabaseAccess.RGetString(reader, "code");
            obj.Description = DatabaseAccess.RGetString(reader, "description");
            obj.Location = DatabaseAccess.RGetString(reader, "location");
            obj.Barcodes = DatabaseAccess.RGetString(reader, "barcodes");
            obj.PhotoId = DatabaseAccess.RGetLong(reader, "photoId");
            obj.LastModified = GRPCUtils.ConvertDate(DatabaseAccess.RGetDateTime(reader, "lastModified"));
        }

        protected override void AddSqlParameters(DbCommand cmd, Cactus obj)
        {
            DatabaseAccess.AddDbValue(cmd, "id", obj.Id);
            DatabaseAccess.AddDbValue(cmd, "code", obj.Code);
            DatabaseAccess.AddDbValue(cmd, "description", obj.Description);
            DatabaseAccess.AddDbValue(cmd, "location", obj.Location);
            DatabaseAccess.AddDbValue(cmd, "barcodes", obj.Barcodes);
            DatabaseAccess.AddDbValue(cmd, "lastModified", GRPCUtils.ConvertDate(obj.LastModified));

            long photoId = 1;
            if (obj.PhotoId > 1) 
            {
                photoId = obj.PhotoId;
            }
            DatabaseAccess.AddDbValue(cmd, "photoId", photoId);
        }

        protected override void SetId(Cactus obj, long id) => obj.Id = id;
        protected override long GetId(Cactus obj) => obj.Id;
    }
}
