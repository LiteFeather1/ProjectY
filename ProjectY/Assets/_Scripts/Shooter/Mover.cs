using UnityEngine;
using ScriptableObjectEvents;

namespace ProjectY
{
    public abstract class Mover : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _backSpeedMultiplier;

        [Header("Type")]
        [SerializeField] protected TargetType _badOrGood = TargetType.Bad;
        public TargetType Type => _badOrGood;

        [Header("Event")]
        [SerializeField] protected TargetEvent _event;

        public abstract void Move();

        public abstract void MoveBack(float speedMultiplier = 1);

        public virtual void ReturnToPool()
        {
            _event.Raise(this);
            MoveBack(_backSpeedMultiplier);
        }
    }
}