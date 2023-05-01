using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjectEvents;

namespace SkeeBall
{
    public class SkeeBallManager : MonoBehaviour
    {
        [Header("Ball")]
        [SerializeField] private Ball _ball;
        private Queue<Ball> _thrownBalls;
        [SerializeField] private int _startingBalls = 9;
        private List<Ball> _currentBalls;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private float _delayBetweenSpawns = 0.25f;
        private WaitForSeconds _delayWait;
        private bool _respawning = false;

        public int BallCount => _currentBalls != null ? _currentBalls.Count : 0;

        [Header("Score")]
        [Tooltip("Every time the player scores this amount he gains balls back")]
        [SerializeField] private int _howMuchScoreToGainBall = 100;// Better name pls
        [SerializeField] private int _ballsToGain = 3;
        private int _timesThatGainBall = 1;
        [SerializeField] private GameObject _ballListerner;

        [Header("Special Score")]
        [SerializeField] private int _ballsToGainFromSpecialScore = 1;

        [Header("End Game")]
        [SerializeField] private VoidEvent _endGame;
        private bool _gameStarted;

        private void Start() => _delayWait = new(_delayBetweenSpawns);

        private void Awake()
        {
            _thrownBalls = new();
            _currentBalls = new();
        }

        [ContextMenu("Start Game")]
        //Event listener
        public void StartGame() => StartCoroutine(SpawnBalls(_startingBalls));

        private IEnumerator SpawnBalls(float amount)
        {
            _gameStarted = true;
            _ballListerner.SetActive(true);
            for (int i = 0; i < amount; i++)
            {
                _currentBalls.Add(Instantiate(_ball, _ballSpawnPoint.transform.position, Quaternion.identity));
                yield return _delayWait;
            }

            _respawning = false;
        }

        //Event Listener. Listens to the special Score
        public void GiveBallsSpecialScore() => StartCoroutine(GiveBallsCo(_ballsToGainFromSpecialScore));

        //Event Listener. Listens to score manager score update
        public void GiveBalls(float scoreAmount)
        {
            int i = _howMuchScoreToGainBall * _timesThatGainBall;

            if (scoreAmount / i >= 1)
            {
                _respawning = true;
                DestroyExtraBalls();
                _timesThatGainBall++;
                StartCoroutine(GiveBallsCo(_ballsToGain));
            }
        }

        private void DestroyExtraBalls()
        {
            for (int i = _currentBalls.Count - 1; i >= 0; i--)
            {
                QueueBall(_currentBalls[i]);
            }
        }

        IEnumerator GiveBallsCo(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (_currentBalls.Count >= 3)
                    break;

                if (_thrownBalls.Count == 0)
                {
                    StartCoroutine(SpawnBalls(amount - i));
                    yield break;
                }

                _currentBalls.Add(_thrownBalls.Dequeue().Appear(_ballSpawnPoint));
                yield return _delayWait;
            }

            _respawning = false;
        }

        private void QueueBall(Ball ball)
        {
            _currentBalls.Remove(ball);
            _thrownBalls.Enqueue(ball);
        }

        //Event Listener.Listens to when a ball is stops moving 
        public void BallThrown(Ball ball)
        {
            QueueBall(ball);
            EndGame();
        }

        public void EndGame()
        {
            // might need a clouse here
            if (_respawning || !_gameStarted)
                return;

            if (_currentBalls.Count == 0)
            {
                _gameStarted = false;
                print("No more balls");
                _endGame.Raise();
                GameEnded();
                _ballListerner.SetActive(false);

                foreach (var ball in _thrownBalls)
                {
                    ball.enabled = false;
                }
            }
        }

        public void GameEnded()
        {
            if (_currentBalls.Count == 0)
                return;

            for (int i = _currentBalls.Count - 1; i >= 0; i--)
            {
                _currentBalls.Remove(_currentBalls[i]);
                _thrownBalls.Enqueue(_currentBalls[i]);
            }

            foreach (var ball in _thrownBalls)
            {
                ball.enabled = false;
            }
        }
    }
}
