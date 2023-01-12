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
        private int _currentBallCount;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private float _delayBetweenSpawns = 0.25f;
        private WaitForSeconds _delayWait;

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
            StartCoroutine(SpawnBalls(_startingBalls));
        }

        private IEnumerator SpawnBalls(float amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_ball, _ballSpawnPoint.transform.position, Quaternion.identity);
                _currentBallCount++;
                yield return _delayWait;
            }
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
                _timesThatGainBall++;
                StartCoroutine(GiveBallsCo(_ballsToGain));
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
                _thrownBalls.Dequeue().Appear(_ballSpawnPoint);
                _currentBallCount++;
                yield return _delayWait;
            }
        }

        //Event Listener.Listens to when a ball is stops moving 
        public void BallThrown(Ball ball)
        {
            _currentBallCount--;
            _thrownBalls.Enqueue(ball);
            EndGame();
        }

        public void EndGame()
        {
            if(_currentBallCount == 0)
            {
                print("No more balls");
                _endGame.Raise();
            }
        }
    }
}
