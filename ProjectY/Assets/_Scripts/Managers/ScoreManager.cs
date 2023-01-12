using UnityEngine;
using ScriptableObjectEvents;
using static UnityEngine.Rendering.DebugUI;

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

    public void SaveScoreToPrefs()
    {
        if (_score > PlayerPrefs.GetFloat(_game.ToString()))
            PlayerPrefs.SetFloat(_game.ToString(), _score);
    }

    //Todo Turn Score into tickets
}
