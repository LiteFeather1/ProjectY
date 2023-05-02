﻿using UnityEngine;
using ScriptableObjectEvents;

public class StarGame : MonoBehaviour
{
    [SerializeField] private VoidEvent _gameToStart;
    private bool _gameStarted;

    public virtual void GameStarted()
    {
        if (_gameStarted)
            return;

        _gameStarted = true;
        _gameToStart.Raise();
    }

    public virtual void GameEnded() => _gameStarted = false;
}
