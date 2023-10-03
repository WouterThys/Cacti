using CactiServer.Repos;
using Common.Services.Photoes;
using Grpc.Core;

namespace CactiServer.Services
{
    public class PhotoService : Photos.PhotosBase
    {

        private readonly ILogger<PhotoService> _logger;
        private readonly PhotoRepo _repo;

        public PhotoService(ILogger<PhotoService> logger, PhotoRepo repo) 
        {
            _logger = logger;
            _repo = repo;
        }

        public override async Task<GetAllPhotoReply> GetAll(GetAllPhotoRequest request, ServerCallContext context)
        {
            _logger.LogDebug("GetAll");

            var data = await _repo.GetBy(request.Ids);
            var reply = new GetAllPhotoReply();
            reply.Data.AddRange(data);
            return reply;
        }

        public override async Task<UpdatePhotoReply> Update(UpdatePhotoRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Update");

            var saved = await _repo.Save(request.Data);
            var reply = new UpdatePhotoReply
            {
                Data = saved
            };
            return reply;
        }

        public override async Task<DeletePhotoReply> Delete(DeletePhotoRequest request, ServerCallContext context)
        {
            _logger.LogDebug("Delete");

            var deleted = await _repo.Delete(request.Id);
            var reply = new DeletePhotoReply
            {
                Id = deleted
            };
            return reply;
        }
    }
}
