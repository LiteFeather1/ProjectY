using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartGameOnGrab : StarGame
{
    [SerializeField] private XRGrabInteractable _interactable;

    private void OnEnable()
    {
        _interactable.firstSelectEntered.AddListener(GameStarted);
    }

    private void OnDisable() => RemoveListener();

    private void RemoveListener()
    {
        _interactable.firstSelectEntered.RemoveListener(GameStarted);
    }

    private void GameStarted(SelectEnterEventArgs arg0) => GameStarted();

    public override void GameStarted()
    {
        base.GameStarted();
        RemoveListener();
    }
}
