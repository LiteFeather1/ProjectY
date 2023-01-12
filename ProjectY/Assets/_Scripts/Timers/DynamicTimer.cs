using UnityEngine;

namespace ProjectY
{
    public class DynamicTimer : Timer
    {
        public override float ElapsedTime { get => base.ElapsedTime; set => base.ElapsedTime = value; }
        public override float Time { get => _timer; protected set => _timer.SetValue(value); }
        public float Multiplier { get => _multiplier; set => _multiplier = value; }

        [SerializeField] private FloatVariable _timer;
        [SerializeField] private FloatVariable _timeElapsed;
        [SerializeField] private float _multiplier;

        protected override void TickTime()
        {
            ElapsedTime += UnityEngine.Time.deltaTime * _multiplier;
            _timeElapsed.SetValue(ElapsedTime);
        }
    }

 }