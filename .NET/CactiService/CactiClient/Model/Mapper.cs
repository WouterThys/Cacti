using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient.Model
{
    public static class Mapper
    {

        public static CactusView Map(Common.Proto.Cactus cactus) 
        {
            return new CactusView() 
            { 
                Id = cactus.Id,
                Code = cactus.Code,
                Description = cactus.Description,
                AndroidId = cactus.AndroidId,
                Location = cactus.Location,
                Barcodes = cactus.Barcodes,
                PhotoId = cactus.PhotoId,
                LastModified = GRPCUtils.ConvertDate(cactus.LastModified),
            };
        }

        public static Common.Proto.Cactus Map(CactusView cactus)
        {
            return new Common.Proto.Cactus()
            {
                Id = cactus.Id,
                Code = cactus.Code,
                Description = cactus.Description,
                AndroidId = cactus.AndroidId,
                Location = cactus.Location,
                Barcodes = cactus.Barcodes,
                PhotoId = cactus.PhotoId,
                LastModified = GRPCUtils.ConvertDate(cactus.LastModified),
            };
        }

    }
}
