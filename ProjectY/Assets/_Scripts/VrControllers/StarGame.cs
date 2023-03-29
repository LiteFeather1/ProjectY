using UnityEngine;
using ScriptableObjectEvents;

public class StarGame : MonoBehaviour
{
    [SerializeField] private GamesEvent _changeGame;
    [SerializeField] private Games _gameToSet;
    private bool _gameStarted;

    public virtual void GameStarted()
    {
        if (_gameStarted)
            return;

        _gameStarted = true;
        _changeGame.Raise(_gameToSet);
    }

    public void GameEnded() => _gameStarted = false;
}
