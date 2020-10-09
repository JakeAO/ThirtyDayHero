using System;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class StateContainer
    {
        public event Action<Type, string> ContainerChanged;

        private readonly Dictionary<(Type, string), object> _container = new Dictionary<(Type, string), object>();

        public void Set<T>(T value, string key = null)
        {
            Type type = typeof(T);
            var keyArg = (type, key);

            _container[keyArg] = value;

            NotifyContainerChanged(type, key);
        }

        public bool TryGet<T>(out T value, string key = null)
        {
            Type type = typeof(T);
            var keyArg = (type, key);

            if (_container.TryGetValue(keyArg, out var objValue) &&
                objValue is T tValue)
            {
                value = tValue;
                return true;
            }

            value = default;
            return false;
        }

        public T Get<T>(string key = null)
        {
            Type type = typeof(T);
            var keyArg = (type, key);

            if (_container.TryGetValue(keyArg, out var objValue) &&
                objValue is T tValue)
            {
                return tValue;
            }

            throw new KeyNotFoundException($"StateContainer did not contain a value for Type: {type}, Key: {key}");
        }

        public void Clear<T>(string key = null)
        {
            Type type = typeof(T);
            var keyArg = (type, key);

            if (_container.Remove(keyArg))
            {
                NotifyContainerChanged(type, key);
            }
        }

        public void ClearAll()
        {
            _container.Clear();

            NotifyContainerChanged(null, string.Empty);
        }

        private void NotifyContainerChanged(Type t, string s) => ContainerChanged?.Invoke(t, s);
    }
}