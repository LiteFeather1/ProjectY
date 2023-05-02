using System.Collections.Generic;
using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace Shooter
{
    public class ShooterManager : MonoBehaviour
    {
        [Header("Targets")]
        [SerializeField] private Vector2Int _batchRange = new(1,6);
        public List<Mover> _targetPool = new();
        private readonly List<Mover> _currentBadTargetsFlipped = new();
        private readonly List<Mover> _currentGoodTargetsFlipped = new();

        public int TargetCount => _targetPool.Count;

        [Header("Timers")]
        [SerializeField] private Timer _endGameTimer;
        [SerializeField] private VoidEvent _gameEnded;
        [SerializeField] private FloatVariable _currentTime;
        [SerializeField] private Timer _flipTargets;
        [SerializeField] private Timer _flipTargetsBack;

        private void OnEnable()
        {
            _endGameTimer.TimeEvent += EndGame;
            _flipTargets.TimeEvent += FlipTargets;
            _flipTargetsBack.TimeEvent += FlipTargetsBack;
        }

        private void OnDisable()
        {
            _endGameTimer.TimeEvent -= EndGame;
            _flipTargets.TimeEvent -= FlipTargets;
            _flipTargetsBack.TimeEvent -= FlipTargetsBack;
        }

        //Event
        [ContextMenu("Start Game")]
        public void StartGame()
        {
            _endGameTimer.Restart();
            _flipTargets.Restart();
        }

        private void Update() => UpdateTimeLeft();

        private void UpdateTimeLeft()
        {
            if (!_endGameTimer.CanTick)
                return;

            float startTime = _endGameTimer.Time;
            _currentTime.SetValue(MathHelper.Map(_endGameTimer.ElapsedTime, 0, startTime, startTime, 0));
        }

        public void EndGame()
        {
            DisableTimers(_flipTargetsBack);
            DisableTimers(_endGameTimer);
            FlipTargetsBack();
            _currentTime.SetValue(0);
            _gameEnded.Raise();
            DisableTimers(_flipTargets);
        }

        private void DisableTimers(Timer timer)
        {
            timer.StopAndReset();
        }

        [ContextMenu("Flip")]
        public void FlipTargets()
        {
            _flipTargets.StopAndReset();
            _flipTargetsBack.Continue();

            int amountToFlip = Random.Range(_batchRange.x, _batchRange.y);

            for (int i = 0; i < amountToFlip; i++)
            {
                int index = Random.Range(0, _targetPool.Count);

                if (index >= _targetPool.Count)
                    continue;

                Mover iFlipper = _targetPool[index];
                RemoveFromPool(iFlipper);
                iFlipper.Move();
            }
        }

        private void RemoveFromPool(Mover iFlipper)
        {
            _targetPool.Remove(iFlipper);
            if (iFlipper.Type == TargetType.Bad)
                _currentBadTargetsFlipped.Add(iFlipper);
            else
                _currentGoodTargetsFlipped.Add(iFlipper);
        }

        [ContextMenu("FlipBack")]
        public void FlipTargetsBack()
        {
            _flipTargets.Continue();
            _flipTargetsBack.StopAndReset();

            if (_currentBadTargetsFlipped.Count > 0)
                FlipTargetBackLoop(_currentBadTargetsFlipped);
            if (_currentGoodTargetsFlipped.Count > 0)
                FlipTargetBackLoop(_currentGoodTargetsFlipped);
        }

        private void FlipTargetBackLoop(List<Mover> targetFlippers, float speedMultiplier = 1)
        {
            for (int i = targetFlippers.Count - 1; i >= 0; i--)
            {
                Mover iFlipper = targetFlippers[i];
                AddBackToPool(targetFlippers, iFlipper);
                iFlipper.MovedDown(speedMultiplier);
            }
        }

        private void AddBackToPool(List<Mover> targetFlippers,Mover iFlipper)
        {
            if (iFlipper is not TargetFlipper)
                return;

            _targetPool.Add(iFlipper);
            targetFlippers.Remove(iFlipper);
        }

        private void AddBackToPool(Mover iFlipper)
        {
            if (iFlipper is not TargetFlipper)
                return;

            _targetPool.Add(iFlipper);
            if (iFlipper.Type == TargetType.Bad)
                _currentBadTargetsFlipped.Remove(iFlipper);
            else
                _currentGoodTargetsFlipped.Remove(iFlipper);
        }

        public void AddToPoolRaw(Mover mover)
        {
            if (mover is not TargetFlipper)
                return;

            _targetPool.Add(mover);
        }

        //Event Listener
        public void AddBackToPoolPublic(Mover flipper)
        {
            if (flipper is not TargetFlipper)
                return;

            AddBackToPool(flipper);

            if (_currentBadTargetsFlipped.Count == 0)
            {
                _flipTargets.Continue();
                _flipTargetsBack.StopAndReset();

                if (_currentGoodTargetsFlipped.Count > 0)
                    FlipTargetBackLoop(_currentGoodTargetsFlipped, 2);
            }
        }

        //Event listener 
        public void AddSecondToTimer(float timeToAdd) => _endGameTimer.ChangeTime(timeToAdd);
    }
}
