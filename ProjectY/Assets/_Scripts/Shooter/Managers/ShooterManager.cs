using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace Shooter
{
    public class ShooterManager : MonoBehaviour
    {
        [Header("Targets")]
        [ContextMenuItem("Get all targets in scene", nameof(GetAllTargetsInScene))]
        [SerializeField] private List<Mover> _targetPool = new();
        private readonly List<Mover> _currentBadTargetsFlipped = new();
        private readonly List<Mover> _currentGoodTargetsFlipped = new();
        [SerializeField] private Vector2Int _batchRange = new(1,6);

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

        private void Update()
        {
            UpdateTimeLeft();
        }

        private void UpdateTimeLeft()
        {
            if(!_endGameTimer.CanTick)
                return;
            float startTime = _endGameTimer.Time;
            _currentTime.SetValue(MathHelper.Map(_endGameTimer.ElapsedTime, 0, startTime, startTime, 0));
        }

        public void EndGame()
        {
            DisableTimers(_flipTargets);
            DisableTimers(_flipTargetsBack);
            DisableTimers(_endGameTimer);
            _currentTime.SetValue(0);
            _gameEnded.Raise();
            FlipTargetsBack();
            gameObject.SetActive(false);
        }

        private void DisableTimers(Timer timer)
        {
            timer.StopAndReset();
            timer.enabled = false;
        }

        private void GetAllTargetsInScene()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(this);
#endif
            _targetPool = FindObjectsOfType<Mover>().ToList();
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
                iFlipper.MoveBack(speedMultiplier);
            }
        }

        private void AddBackToPool(List<Mover> targetFlippers,Mover iFlipper)
        {
            _targetPool.Add(iFlipper);
            targetFlippers.Remove(iFlipper);
        }

        private void AddBackToPool(Mover iFlipper)
        {
            _targetPool.Add(iFlipper);
            if (iFlipper.Type == TargetType.Bad)
                _currentBadTargetsFlipped.Remove(iFlipper);
            else
                _currentGoodTargetsFlipped.Remove(iFlipper);
        }

        //Event Listener
        public void AddBackToPoolPublic(Mover flipper)
        {
            AddBackToPool(flipper);
            if (_currentBadTargetsFlipped.Count == 0)
            {
                _flipTargets.Continue();
                _flipTargetsBack.StopAndReset();

                if (_currentGoodTargetsFlipped.Count > 0)
                    FlipTargetBackLoop(_currentGoodTargetsFlipped, 2);

                // Can Add A special score here 
                // Like if the player shot all targets before the timer to flip back
            }
        }

        //Event listener 
        public void AddSecondToTimer(float timeToAdd)
        {
            _endGameTimer.ChangeTime(timeToAdd);
        }
    }
}
