using Common.Services;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CactiClient.WebClient
{
    public interface ICallbackManager
    {
        public void OnChanged<T>(UpdateAction type, T data); //where T : IBaseObject;
        public void RegisterCallback(INotifyChanged callback, UpdateAction? type = null, string? objectName = null);
        public void UnregisterCallback(INotifyChanged callback);
    }

    public interface INotifyChanged
    {
        public Task NotifyChanged<T>(UpdateAction type, T data);
    }

    public class Callback
    {
        public INotifyChanged? NotifyChanged { get; set; }
        public UpdateAction? Type { get; set; }
        public string? ObjectName { get; set; }
    }


    public class CallbackService : IAsyncDisposable
    {
        private static CallbackService? instance;
        public static CallbackService Initialize(GrpcChannel channel)
        {
            instance = new CallbackService(channel);
            return instance;
        }
        public static CallbackService GetInstance()
        {
            if (instance == null) throw new NullReferenceException("Instance is null");
            return instance;
        }


        private readonly Callbacks.CallbacksClient _CallbackClient;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private AsyncServerStreamingCall<UpdateMessage>? _callbackStream;
        private Task? _responseTask;

        private readonly List<Callback> _registeredCallbacks = new();

        private CallbackService(GrpcChannel channel)
        {
            _CallbackClient = new Callbacks.CallbacksClient(channel);
        }

        public async ValueTask DisposeAsync()
        {
            _cancellationTokenSource.Cancel();
            try
            {
                if (_responseTask != null)
                {
                    await _responseTask.ConfigureAwait(false);
                }
            }
            finally
            {
            }
        }

        public void Register()
        {
            var request = new SubscribeRequest();
            _callbackStream = _CallbackClient.Subscribe(request);
            _responseTask = HandleCallback(_callbackStream, _cancellationTokenSource.Token);
        }

        public void RegisterCallback(INotifyChanged callback, UpdateAction? type = null, string? objectName = null)
        {
            _registeredCallbacks.Add(new Callback()
            {
                NotifyChanged = callback,
                Type = type,
                ObjectName = objectName
            });
            //_logger.LogInformation("Added callback, current size is {size}", _registeredCallbacks.Count);
        }

        public void UnregisterCallback(INotifyChanged callback)
        {
            _registeredCallbacks.RemoveAll((x) => x.NotifyChanged == callback);
            //_logger.LogInformation("Removed callback, current size is {size}", _registeredCallbacks.Count);
        }


        private async Task HandleCallback(AsyncServerStreamingCall<UpdateMessage> callbackStream, CancellationToken token)
        {
            var stream = callbackStream.ResponseStream;

            await foreach (var update in stream.ReadAllAsync(token))
            {
                switch (update.DataCase)
                {
                    case UpdateMessage.DataOneofCase.Cactus:
                        OnChanged(update.Action, update.Cactus);
                        break;


                    case UpdateMessage.DataOneofCase.None:
                        Console.WriteLine("A change without happened.....");
                        break;
                }

            }
        }

        private void OnChanged<T>(UpdateAction type, T data) //where T : IBaseObject
        {
            foreach (var callback in _registeredCallbacks)
            {
                if (callback.Type == null && string.IsNullOrEmpty(callback.ObjectName))
                {
                    callback.NotifyChanged?.NotifyChanged(type, data);
                    continue;
                }

                if (callback.Type == type)
                {
                    if (string.IsNullOrEmpty(callback.ObjectName))
                    {
                        callback.NotifyChanged?.NotifyChanged(type, data);
                        continue;
                    }

                    if (callback.ObjectName.Equals(typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                    {
                        callback.NotifyChanged?.NotifyChanged(type, data);
                    }
                }

            }
        }
    }
}
