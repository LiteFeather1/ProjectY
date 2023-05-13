using UnityEngine;
using ScriptableObjectEvents;

public class ScoreManager : MonoBehaviour
{
    private float _score;
    private float _lastScore;
    [SerializeField] private FloatEvent _scoreUpdated;
    [SerializeField] private FloatEvent _lastScoreUpdate;
    [SerializeField] private FloatEvent _bestScoreUpdate;
    [SerializeField] private Games _game;

    private string BestScore => $"Best Score {_game}"; 

    //Event Listener to when score event is Raised
    public void ChangeScore(float amount)
    {
        _score += amount;
        _score = Mathf.Clamp(_score, 0f, float.MaxValue);
        _scoreUpdated.Raise(_score);
    }

    public void GameEnded()
    {
        _lastScore = _score;
        _score = 0;
        _scoreUpdated.Raise(_score);
        _lastScoreUpdate.Raise(_lastScore);
        _bestScoreUpdate.Raise(GetBestScore());
    }

    private float GetBestScore()
    {
        float bestScore = PlayerPrefs.GetFloat(BestScore, 0f);

        if (_score > bestScore)
        {
            PlayerPrefs.SetFloat(BestScore, _score);
            return _score;
        }

        return bestScore;
    }
}
