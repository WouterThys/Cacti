using Common.Services;
using Google.Api;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace CactiClient.WebClient
{
    public class FileService
    {
        private static FileService? instance;
        public static FileService Initialize(GrpcChannel channel)
        {
            instance = new FileService(channel);
            return instance;
        }
        public static FileService GetInstance()
        {
            if (instance == null) throw new NullReferenceException("Instance is null");
            return instance;
        }


        private readonly Files.FilesClient _client;
        private readonly object _locker = new();
        private readonly Dictionary<string, string> tempFiles = new();

        private readonly string _baseDir;

        private FileService(GrpcChannel channel)
        {
            _client = new Files.FilesClient(channel);

            _baseDir = Properties.Settings.Default.LocalBaseDir;

            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (string.IsNullOrEmpty(_baseDir)) 
            {
                _baseDir = Path.Combine(appdata, "CactiClient", "Photos");
            }
            else 
            {
                _baseDir = _baseDir.Replace("%appdata%", appdata);
            }

            Directory.CreateDirectory(_baseDir);
        }

        public void Clear()
        {
            lock (_locker)
            {
                tempFiles.Clear();
            }
        }

        public string GetBasePath()
        {
            return _baseDir;
        }

        public async Task<string> Load(string path, Action<int>? progressCallback = null)
        {
            string localPath = "";

            // Check valid
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }

            // Search if temp file
            if (!string.IsNullOrEmpty(path) && tempFiles.ContainsKey(path))
            {
                return tempFiles[path];
            }

            // Search local
            localPath = Path.Combine(_baseDir, path);

            if (!File.Exists(localPath))
            {

                // Load from server
                var request = new LoadRequest() { Path = path };
                var responseStream = _client.Load(request);
                var totalSize = 0;
                var totalWritten = 0;

                var meta = await responseStream.ResponseHeadersAsync;
                if (meta != null)
                {
                    var totalStr = meta.GetValue("TotalSize");
                    _ = int.TryParse(totalStr, out totalSize);
                }

                using Stream dest = File.OpenWrite(localPath);
                await foreach (var result in responseStream.ResponseStream.ReadAllAsync())
                {
                    if (result != null)
                    {
                        dest.Write(result.Data.Data.ToByteArray(), 0, (int)result.Data.Size);

                        if (totalSize > 0 && progressCallback != null)
                        {
                            totalWritten += (int)result.Data.Size;
                            int percent = (int)((int)(totalWritten * 100) / totalSize);
                            progressCallback.Invoke(percent);
                        }
                    }
                }
            }

            lock (_locker)
            {
                tempFiles.Add(path, localPath);
            }

            return localPath;
        }
 
    
        public async Task<string> Save(string path, Action<int>? progressCallback = null) 
        {
            if (!File.Exists(path)) return "";

            var fileInfo = new FileInfo(path);
            var meta = new Metadata
            {
                { "TotalSize", fileInfo.Length.ToString() }
            };

            var request = new SaveRequest
            {
                Path = fileInfo.Name
            };

            using var streamingCall = _client.Save(meta);
           
            await streamingCall.RequestStream.WriteAsync(request);

            // Read file into stream
            using Stream source = File.OpenRead(path);
            byte[] buffer = new byte[4096];
            int bytesRead;
            var totalSize = fileInfo.Length;
            var totalWritten = 0;

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                FileData fileData = new()
                {
                    Size = (uint)bytesRead,
                    Data = ByteString.CopyFrom(buffer)
                };
                request.Data = fileData;
                await streamingCall.RequestStream.WriteAsync(request);

                // Progress
                if (totalSize > 0 && progressCallback != null)
                {
                    totalWritten += bytesRead;
                    int percent = (int)((int)(totalWritten * 100) / totalSize);
                    progressCallback.Invoke(percent);
                }

            }
            await streamingCall.RequestStream.CompleteAsync();

            var reply = await streamingCall;

            return reply.Path;
        }
    }
}
