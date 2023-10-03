using Database;

namespace CactiServer.Managers
{
    public interface ICallbackManager
    {
        public void OnChanged<T>(DbActionType type, T data); //where T : IBaseObject;
        public void RegisterCallback(INotifyChanged callback, DbActionType? type = null, string? objectName = null);
        public void UnregisterCallback(INotifyChanged callback);
    }

    public interface INotifyChanged
    {
        public Task NotifyChanged<T>(DbActionType type, T data);
    }

    public class Callback
    {
        public INotifyChanged? NotifyChanged { get; set; }
        public DbActionType? Type { get; set; }
        public string? ObjectName { get; set; }
    }

    public class CallbackManager : ICallbackManager
    {
        private readonly ILogger<CallbackManager> _logger;
        private readonly List<Callback> _registeredCallbacks = new();


        public CallbackManager(ILogger<CallbackManager> logger)
        {
            _logger = logger;
        }

        public void RegisterCallback(INotifyChanged callback, DbActionType? type = null, string? objectName = null)
        {
            _registeredCallbacks.Add(new Callback()
            {
                NotifyChanged = callback,
                Type = type,
                ObjectName = objectName
            });
            _logger.LogInformation("Added callback, current size is {size}", _registeredCallbacks.Count);
        }

        public void UnregisterCallback(INotifyChanged callback)
        {
            _registeredCallbacks.RemoveAll((x) => x.NotifyChanged == callback);
            _logger.LogInformation("Removed callback, current size is {size}", _registeredCallbacks.Count);
        }


        public void OnChanged<T>(DbActionType type, T data) //where T : IBaseObject
        {
            _logger.LogTrace("OnChanged: {type}: {data}", type, data);

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
