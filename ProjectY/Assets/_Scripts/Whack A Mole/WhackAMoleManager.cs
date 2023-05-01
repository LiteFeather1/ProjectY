using ProjectY;
using ScriptableObjectEvents;
using Shooter;
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
        private List<Mover> _currentMoles = new();
        [SerializeField] private Vector2Int _batchRange = new(1, 3);

        [Header("Timers")]
        [SerializeField] private float _gameTime = 120f;
        [SerializeField] private DynamicTimer _endGameTimer;
        [SerializeField] private VoidEvent _gameEnded;
        [SerializeField] private FloatVariable _currentTime;
        [SerializeField] private Timer _popMoles;
        [SerializeField] private Timer _moveDownTimer;

        private bool _gameStarted = false;

        private void OnEnable()
        {
            _endGameTimer.TimeEvent += EndGame;
            _popMoles.TimeEvent += PopMoles;
            _moveDownTimer.TimeEvent += MoveMolesDown;
        }

        private void OnDisable()
        {
            _endGameTimer.TimeEvent -= EndGame;
            _popMoles.TimeEvent -= PopMoles;
            _moveDownTimer.TimeEvent -= MoveMolesDown;
        }

        private void Update()
        {
            UpdateTimeLeft();
        }

        [ContextMenu("Start Gaem")]
        public void StartGame()
        {
            _gameStarted = true;
            _endGameTimer.Restart();
            _endGameTimer.SetTime(_gameTime);
            _popMoles.Restart();
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
            _gameEnded.Raise();
            DisableTimers(_popMoles);
            DisableTimers(_moveDownTimer);
            DisableTimers(_endGameTimer);

            int count = _currentMoles.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                Mover iMover = _currentMoles[i];
                AddBackToPool(iMover);
                iMover.MovedDown();
            }

            _currentTime.SetValue(0);
            _gameStarted = false;
        }

        private void DisableTimers(Timer timer)
        {
            timer.StopAndReset();
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

            int amountToFlip = Random.Range(_batchRange.x, _batchRange.y + 1);

            _endGameTimer.Multiplier = amountToFlip;

            for (int i = 0; i < amountToFlip; i++)
            {
                int index = Random.Range(0, _molesPool.Count);

                Mover imover = _molesPool[index];
                RemoveFromPool(imover);

                imover.Move();
            }

            _moveDownTimer.Continue();
        }

        public void MoveMolesDown()
        {
            _moveDownTimer.StopAndReset();
            _popMoles.Continue();

            int count = _currentMoles.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                Mover iMover = _currentMoles[i];
                AddBackToPool(iMover);
                iMover.MovedDown();
            }
        }

        private void RemoveFromPool(Mover iFlipper)
        {
            _molesPool.Remove(iFlipper);
            _currentMoles.Add(iFlipper);
        }

        private void AddBackToPool(Mover iFlipper)
        {
            if (iFlipper is not WhackAMoleMover)
                return;

            _molesPool.Add(iFlipper);
            _currentMoles.Remove(iFlipper);
            _endGameTimer.Multiplier--;

            if (_endGameTimer.Multiplier < 0)
                _endGameTimer.Multiplier = 0;
        }

        public void AddToPoolRaw(Mover mover)
        {
            if (mover is not WhackAMoleMover)
                return;

            _molesPool.Add(mover);
        }

        //Event Listener
        public void AddBackToPoolPublic(Mover mover)
        {
            if (mover is not WhackAMoleMover)
                return;

            //AddBackToPool(mover);
            //if (_currentMoles.Count == 0)
            //{
            //    _popMoles.Continue();
            //}
        }

        //Event listener 
        public void AddSecondToTimer(float timeToAdd)
        {
            _endGameTimer.ChangeTime(timeToAdd);
        }
    }
}