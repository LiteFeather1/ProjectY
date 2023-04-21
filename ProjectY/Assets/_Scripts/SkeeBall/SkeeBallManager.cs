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
        private readonly Queue<Ball> _thrownBalls = new();
        [SerializeField] private int _startingBalls = 9;
        private readonly List<Ball> _currentBalls = new();
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private float _delayBetweenSpawns = 0.25f;
        private WaitForSeconds _delayWait;
        private bool _respawning = false;
        public int BallCount => _currentBalls.Count;

        [Header("Score")]
        [Tooltip("Every time the player scores this amount he gains balls back")]
        [SerializeField] private int _howMuchScoreToGainBall = 100;// Better name pls
        [SerializeField] private int _ballsToGain = 3;
        private int _timesThatGainBall = 1;

        [Header("Special Score")]
        [SerializeField] private int _ballsToGainFromSpecialScore = 1;

        [Header("End Game")]
        [SerializeField] private VoidEvent _endGame;


        private void Start()
        {
            _delayWait = new(_delayBetweenSpawns);
        }

        [ContextMenu("Start Game")]
        public void StartGame() => StartGame(Games.SkeeBall);

        //Event listener
        public void StartGame(Games game)
        {
            if (game == Games.SkeeBall)
                StartCoroutine(SpawnBalls(_startingBalls));
        }

        private IEnumerator SpawnBalls(float amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _currentBalls.Add(Instantiate(_ball, _ballSpawnPoint.transform.position, Quaternion.identity));
                yield return _delayWait;
            }
            _respawning = false;
        }

        //Event Listener. Listens to the special Score
        public void GiveBallsSpecialScore()
        {
            StartCoroutine(GiveBallsCo(_ballsToGainFromSpecialScore));
        }

        //Event Listener. Listens to score manager score update
        public void GiveBalls(float scoreAmount)
        {
            int i = _howMuchScoreToGainBall * _timesThatGainBall;

            if(scoreAmount / i >= 1)
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
            if (_respawning)
                return;

            if (_currentBalls.Count == 0)
            {
                print("No more balls");
                _endGame.Raise();

                foreach (var ball in _thrownBalls)
                {
                    ball.enabled = false;
                }
            }
        }
    }
}
