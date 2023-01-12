using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ScriptableObjectEvents;

public class StartGameOnGrab : MonoBehaviour
{
    [SerializeField] private VoidEvent _startGame;
    [SerializeField] private XRGrabInteractable _interactable;
    private bool _gameStarted;

    private void OnEnable()
    {
        _interactable.firstSelectEntered.AddListener(_ => GameStarted());
    }

    private void OnDisable()
    {
        _interactable.firstSelectEntered.RemoveListener(_ => GameStarted());
    }

    private void GameStarted()
    {
        if (_gameStarted)
            return;
        _gameStarted = true;
        _startGame.Raise();
        _interactable.firstSelectEntered.RemoveListener(_ => GameStarted());
    }
}
