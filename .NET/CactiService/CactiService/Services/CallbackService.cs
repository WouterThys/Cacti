using CactiServer.Managers;
using Common.Proto;
using Common.Services;
using Database;
using Grpc.Core;

namespace CactiServer.Services
{

    public class CallbackService : Callbacks.CallbacksBase, INotifyChanged
    {
        private readonly ILogger<CallbackService> _logger;
        private readonly ICallbackManager _callbackManager;

        private IServerStreamWriter<UpdateMessage>? _responseStream;


        public CallbackService(ILogger<CallbackService> logger, ICallbackManager callbackManager)
        {
            _logger = logger;
            _callbackManager = callbackManager;
        }

        public override async Task Subscribe(SubscribeRequest request, IServerStreamWriter<UpdateMessage> responseStream, ServerCallContext context)
        {
            // Add subscriber to callback repo
            _responseStream = responseStream;
            _callbackManager.RegisterCallback(this);

            _logger.LogInformation("Subscription started.");

            await AwaitCancellation(context.CancellationToken);

            _callbackManager.UnregisterCallback(this);

            _logger.LogInformation("Subscription finished.");
        }

        public async Task NotifyChanged<T>(DbActionType type, T data)
        {
            if (_responseStream == null) return;
            var actionType = ConvertActionType(type);

            if (actionType == null) return;

            if (data is Cactus c) await WriteUpdateAsync(_responseStream, new UpdateMessage() { Action = (UpdateAction)actionType, Cactus = c });

        }

        private async Task WriteUpdateAsync(IServerStreamWriter<UpdateMessage> stream, UpdateMessage message)
        {
            try
            {
                await stream.WriteAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Callback failed");
            }
        }

        private static UpdateAction? ConvertActionType(DbActionType actionType)
        {
            return actionType switch
            {
                DbActionType.Update => UpdateAction.Update,
                DbActionType.Delete => UpdateAction.Delete,
                DbActionType.Insert => UpdateAction.Insert,
                _ => null,
            };
        }

        private static Task AwaitCancellation(CancellationToken token)
        {
            var completion = new TaskCompletionSource<object>();
            token.Register(() => completion.SetResult(true));
            return completion.Task;
        }


    }
}
