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
            obj.Id = reader.RGetLong("id");
            obj.Code = reader.RGetString("code");
            obj.Description = reader.RGetString("description");
            obj.Location = reader.RGetString("location");
            obj.Barcodes = reader.RGetString("barcodes");
            obj.PhotoId = reader.RGetLong("photoId");
            obj.AndroidId = reader.RGetString("androidId");
            obj.LastModified = GRPCUtils.ConvertDate(reader.RGetDateTime("lastModified"));

            obj.FathersCode = reader.RGetString("fathersCode");
            obj.MothersCode = reader.RGetString("mothersCode");
            obj.CrossingNumber = reader.RGetString("crossingNumber");
        }

        protected override void AddSqlParameters(DbCommand cmd, Cactus obj)
        {
            cmd.AddDbValue("id", obj.Id);
            cmd.AddDbValue("code", obj.Code);
            cmd.AddDbValue("description", obj.Description);
            cmd.AddDbValue("location", obj.Location);
            cmd.AddDbValue("barcodes", obj.Barcodes);
            cmd.AddDbValue("androidId", obj.AndroidId);
            cmd.AddDbValue("lastModified", GRPCUtils.ConvertDate(obj.LastModified));

            cmd.AddDbValue("fathersCode", obj.FathersCode);
            cmd.AddDbValue("mothersCode", obj.MothersCode);
            cmd.AddDbValue("crossingNumber", obj.CrossingNumber);

            long photoId = 1;
            if (obj.PhotoId > 1) 
            {
                photoId = obj.PhotoId;
            }
            cmd.AddDbValue("photoId", photoId);
        }

        protected override void SetId(Cactus obj, long id) => obj.Id = id;
        protected override long GetId(Cactus obj) => obj.Id;
    }
}
