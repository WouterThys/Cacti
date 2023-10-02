using Common.Proto;
using Common.Services.Cactuses;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactiClient.WebClient
{
    public class CactiService
    {
        private readonly Cactuses.CactusesClient _CactusClient;

        public CactiService(GrpcChannel channel) : base()
        {
            _CactusClient = new Cactuses.CactusesClient(channel);
        }

        public async Task <IEnumerable<Cactus>> GetAll() 
        {
            var request = new GetAllCactusRequest();
            var reply = await _CactusClient.GetAllAsync(request);
            return reply.Data;
        }

        public async Task<Cactus> Save(Cactus cactus) 
        {
            var request = new UpdateCactusRequest() { Data = cactus };
            var reply = await _CactusClient.UpdateAsync(request);
            return reply.Data;
        }

        public async Task<long> Delete(long id) 
        {
            var request = new DeleteCactusRequest() { Id = id };
            var reply = await _CactusClient.DeleteAsync(request);
            return reply.Id;
        }
    }
}
