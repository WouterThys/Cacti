using CactiServer.Repos;
using Common.Services.Cactuses;
using Grpc.Core;

namespace CactiServer.Services
{
    public class CactiService : Cactuses.CactusesBase
    {

        private readonly ILogger<CactiService> _logger;
        private readonly CactiRepo _repo;

        public CactiService(ILogger<CactiService> logger, CactiRepo repo) 
        {
            _logger = logger;
            _repo = repo;
        }

        public override async Task<GetAllCactusReply> GetAll(GetAllCactusRequest request, ServerCallContext context)
        {
            _logger.LogDebug("GetAll");

            var data = await _repo.GetBy(request.Ids);
            var reply = new GetAllCactusReply();
            reply.Data.AddRange(data);
            return reply;
        }

        public override async Task<UpdateCactusReply> Update(UpdateCactusRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Update");

            var saved = await _repo.Save(request.Data);
            var reply = new UpdateCactusReply
            {
                Data = saved
            };
            return reply;
        }

        public override async Task<DeleteCactusReply> Delete(DeleteCactusRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Delete");

            var deleted = await _repo.Delete(request.Id);
            var reply = new DeleteCactusReply
            {
                Id = deleted
            };
            return reply;
        }
    }
}
