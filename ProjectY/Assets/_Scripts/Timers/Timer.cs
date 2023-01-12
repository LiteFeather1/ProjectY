using UnityEngine;

namespace ProjectY
{
    public abstract class Timer : MonoBehaviour
    {
        public abstract float Time { get; protected set; }
        protected float _elapsedTime;
        [SerializeField] protected bool _canTick = true;

        public bool CanTick => _canTick;
        public virtual float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }

        public System.Action TimeEvent { get; set; }

        protected virtual void Update()
        {
            if (!_canTick)
                return;
            Ticking();
        }

        protected virtual void Ticking()
        {
            TickTime();
            if (_elapsedTime >= Time)
            {
                Reset_();
                TimeEvent?.Invoke();
            }
        }

        protected virtual void TickTime()
        {
            _elapsedTime += UnityEngine.Time.deltaTime;
        }

        public void ChangeTime(float time) => Time += time;

        public void SetTime(float time) => Time = time;

        /// <summary>
        /// Stops ticking
        /// </summary>
        public void Stop()
        {
            _canTick = false;
        }
        /// <summary>
        /// Starts ticking
        /// </summary>
        public void Continue()
        {
            _canTick = true;
        }
        //not unity reset zz
        /// <summary>
        /// Resets elapsed time to 0
        /// </summary>
        public void Reset_()
        {
            _elapsedTime = 0;
        }
        /// <summary>
        /// Stops ticking and Resets elapsed time to 0
        /// </summary>
        public void StopAndReset()
        {
            Stop();
            Reset_();
        }
        /// <summary>
        /// Starts ticking and resets elapsed time to 0
        /// </summary>
        public void Restart()
        {
            Reset_();
            Continue();
        }
    }
 }