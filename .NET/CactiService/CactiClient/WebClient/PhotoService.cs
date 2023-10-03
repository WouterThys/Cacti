using Common.Proto;
using Common.Services.Photoes;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CactiClient.WebClient
{
    public class PhotoService
    {
        private static PhotoService? instance;
        public static PhotoService Initialize(GrpcChannel channel)
        {
            instance = new PhotoService(channel);
            return instance;
        }
        public static PhotoService GetInstance()
        {
            if (instance == null) throw new NullReferenceException("Instance is null");
            return instance;
        }


        private readonly Photos.PhotosClient _PhotoClient;

        private PhotoService(GrpcChannel channel) : base()
        {
            _PhotoClient = new Photos.PhotosClient(channel);
        }

        public async Task <IEnumerable<Photo>> GetAll() 
        {
            var request = new GetAllPhotoRequest();
            var reply = await _PhotoClient.GetAllAsync(request);
            return reply.Data;
        }

        public async Task<Photo?> Get(long id)
        {
            var request = new GetAllPhotoRequest();
            request.Ids.Add(id);
            var reply = await _PhotoClient.GetAllAsync(request);
            return reply.Data.FirstOrDefault();
        }

        public async Task<Photo> Save(Photo Photo) 
        {
            var request = new UpdatePhotoRequest() { Data = Photo };
            var reply = await _PhotoClient.UpdateAsync(request);
            return reply.Data;
        }

        public async Task<long> Delete(long id) 
        {
            var request = new DeletePhotoRequest() { Id = id };
            var reply = await _PhotoClient.DeleteAsync(request);
            return reply.Id;
        }
    }
}
