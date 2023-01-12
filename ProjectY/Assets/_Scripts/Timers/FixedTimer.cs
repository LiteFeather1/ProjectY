using UnityEngine;

namespace ProjectY
{
    public class FixedTimer : Timer
    {
        [SerializeField] private float _time;
        public override float Time { get => _time; protected set => _time = value; }
    }
}