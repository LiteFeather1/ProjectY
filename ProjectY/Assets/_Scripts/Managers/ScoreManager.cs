using UnityEngine;
using ScriptableObjectEvents;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private FloatEvent _scoreUpdated;
    [SerializeField] private Games _game;

    //Event Listener to when score event is Raised
    public void ChangeScore(float amount)
    {
        _score += amount;
        _score = Mathf.Clamp(_score, 0f, float.MaxValue);
        _scoreUpdated.Raise(_score);
    }
    //Event
    public void SaveScoreToPrefs()
    {
        if (_score > PlayerPrefs.GetFloat(_game.ToString()))
            PlayerPrefs.SetFloat(_game.ToString(), _score);
    }

    public void GameStarted(Games game)
    {
        _game = game;
        _score = 0;
    }


    public void GameEnded()
    {
        _score = 0;
        _scoreUpdated.Raise(_score);
    }
}
