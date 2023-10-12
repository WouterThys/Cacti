using Common.Services;
using Google.Protobuf;
using Grpc.Core;

namespace CactiServer.Services
{
    public class FileService : Files.FilesBase
    {
        private const int FILE_DATA_SIZE = 4096;

        private readonly ILogger<FileService> _logger;

        // TODO: from configuration
        private readonly string _basePath;


        public FileService(ILogger<FileService> logger, IConfiguration configuration)
        {
            _logger = logger;

            var section = configuration.GetSection("Directories");
            if (section != null) 
            {
                _basePath = section.GetValue<string>("BaseDirectory") ?? "";
            }
            if (string.IsNullOrEmpty(_basePath)) 
            {
                _basePath = Path.Combine(Environment.CurrentDirectory, "Photos");
            }

            Directory.CreateDirectory(_basePath);

            _logger.LogInformation("Base directory for files is {dir}", _basePath);
        }

        public override async Task Load(LoadRequest request, IServerStreamWriter<LoadReply> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Load request for {path}", request.Path);
            string path = request.Path;
            if (!File.Exists(path))
            {
                path = Path.Combine(_basePath, path);
                if (!File.Exists(path))
                {
                    throw new IOException($"File does not exist: {request.Path}");
                }
            }

            // Set total size in header
            // Write in chunks of xxxx bytes
            try
            {
                var fileInfo = new FileInfo(path);
                var meta = new Metadata
            {
                { "TotalSize", fileInfo.Length.ToString() }
            };
                await context.WriteResponseHeadersAsync(meta);

                // Read file into stream
                using Stream source = File.OpenRead(path);
                byte[] buffer = new byte[FILE_DATA_SIZE];
                int bytesRead;

                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    FileData fileData = new()
                    {
                        Size = (uint)bytesRead,
                        Data = ByteString.CopyFrom(buffer)

                    };

                    var reply = new LoadReply { Data = fileData };
                    await responseStream.WriteAsync(reply);
                }
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Failed to load file");
                throw;
            }
        }

        public override async Task<SaveReply> Save(IAsyncStreamReader<SaveRequest> requestStream, ServerCallContext context)
        {
            await requestStream.MoveNext();

            string? fileName;
            if (requestStream.Current.ContentCase == SaveRequest.ContentOneofCase.Path)
            {
                fileName = requestStream.Current.Path;
            }
            else
            {
                throw new Exception("Invalid save request..");
            }

            var filePath = Path.Combine(_basePath, fileName);
            using Stream dest = File.OpenWrite(filePath);
            
            await foreach (var req in requestStream.ReadAllAsync())
            {
                FileData? fileData = req.Data;
                if (fileData != null)
                {
                    dest.Write(fileData.Data.ToByteArray(), 0, (int)fileData.Size);
                }
            }

            return new SaveReply() { Path = fileName };
        }


    }
}
