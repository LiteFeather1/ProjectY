using ProjectY;
using ScriptableObjectEvents;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WhackAMole
{
    public class WhackAMoleManager : MonoBehaviour
    {
        [Header("Targets")]
        [ContextMenuItem("Get all targets in scene", nameof(GetAllTargetsInScene))]
        [SerializeField] private List<Mover> _molesPool = new();
        private readonly List<Mover> _currentMoles = new();
        [SerializeField] private Vector2Int _batchRange = new(1, 3);

        [Header("Timers")]
        [SerializeField] private DynamicTimer _endGameTimer;
        [SerializeField] private VoidEvent _gameEnded;
        [SerializeField] private FloatVariable _currentTime;
        [SerializeField] private Timer _popMoles;

        private void OnEnable()
        {
            _endGameTimer.TimeEvent += EndGame;
            _popMoles.TimeEvent += PopMoles;
        }

        private void OnDisable()
        {
            _endGameTimer.TimeEvent -= EndGame;
            _popMoles.TimeEvent -= PopMoles;
        }

        private void Update()
        {
            UpdateTimeLeft();
        }

        private void UpdateTimeLeft()
        {
            if (!_endGameTimer.CanTick)
                return;
            float startTime = _endGameTimer.Time;
            _currentTime.SetValue(MathHelper.Map(_endGameTimer.ElapsedTime, 0, startTime, startTime, 0));
        }

        public void EndGame()
        {
            DisableTimers(_popMoles);
            DisableTimers(_endGameTimer);
            _currentTime.SetValue(0);
            _gameEnded.Raise();
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
            _molesPool = FindObjectsOfType<Mover>().ToList();
        }

        [ContextMenu("Flip")]
        public void PopMoles()
        {
            _popMoles.StopAndReset();

            int amountToFlip = Random.Range(_batchRange.x, _batchRange.y);

            _endGameTimer.Multiplier = amountToFlip;

            for (int i = 0; i < amountToFlip; i++)
            {
                int index = Random.Range(0, _molesPool.Count);

                Mover iFlipper = _molesPool[index];
                RemoveFromPool(iFlipper);

                iFlipper.Move();
            }
        }

        private void RemoveFromPool(Mover iFlipper)
        {
            _molesPool.Remove(iFlipper);
            _currentMoles.Add(iFlipper);
        }

        private void AddBackToPool(Mover iFlipper)
        {
            _molesPool.Add(iFlipper);
            _currentMoles.Remove(iFlipper);
            _endGameTimer.Multiplier--;
            if (_endGameTimer.Multiplier < 0)
                _endGameTimer.Multiplier = 0;
        }

        //Event Listener
        public void AddBackToPoolPublic(Mover flipper)
        {
            AddBackToPool(flipper);
            if (_currentMoles.Count == 0)
            {
                _popMoles.Continue();
            }
        }

        //Event listener 
        public void AddSecondToTimer(float timeToAdd)
        {
            _endGameTimer.ChangeTime(timeToAdd);
        }
    }
}