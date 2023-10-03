using Common.Proto;
using Common.Services.Cactuses;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactiClient.WebClient
{
    public class CactiService
    {
        private static CactiService? instance;
        public static CactiService Initialize(GrpcChannel channel)
        {
            instance = new CactiService(channel);
            return instance;
        }
        public static CactiService GetInstance()
        {
            if (instance == null) throw new NullReferenceException("Instance is null");
            return instance;
        }


        private readonly Cactuses.CactusesClient _CactusClient;

        private CactiService(GrpcChannel channel) : base()
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
