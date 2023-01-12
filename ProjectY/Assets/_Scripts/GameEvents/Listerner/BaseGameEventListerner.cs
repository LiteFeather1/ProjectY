using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
    public abstract class BaseGameEventListerner<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E _gameEvent;
        public E GameEvent { get => _gameEvent ;set => _gameEvent = value; }

        [SerializeField] private UER _unityEventResponse;

        private void OnEnable()
        {
            if (_gameEvent == null)
                return;
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (_gameEvent == null)
                return;
            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            _unityEventResponse?.Invoke(item);
        }
    }
}
