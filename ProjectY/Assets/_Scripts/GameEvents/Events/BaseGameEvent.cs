using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _eventListerners = new();

        public void Raise(T t)
        {
            for (int i = _eventListerners.Count - 1; i >= 0; i--)
            {
                _eventListerners[i].OnEventRaised(t);
            }
        }

        public void RegisterListener(IGameEventListener<T> listerner)
        {
            if (!_eventListerners.Contains(listerner))
            {
                _eventListerners.Add(listerner);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listerner)
        {
            if (_eventListerners.Contains(listerner))
            {
                _eventListerners.Remove(listerner);
            }
        }
    }
}
