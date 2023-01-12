using UnityEngine;

namespace ProjectY
{
    [System.Serializable]
    public class ChangeTimerValue
    {
        [SerializeField] private float _changeAmount;
        [SerializeField] private ScriptableObjectEvents.FloatEvent _amountToAddEvent;

        public ChangeTimerValue(float amountToAdd)
        {
            _changeAmount = amountToAdd;
        }

        public void Change() => _amountToAddEvent.Raise(_changeAmount);
    }
}