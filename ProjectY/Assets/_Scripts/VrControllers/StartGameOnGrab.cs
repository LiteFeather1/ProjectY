using ScriptableObjectEvents;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartGameOnGrab : StarGame
{
    [SerializeField] private XRGrabInteractable _interactable;
    [SerializeField] private VoidEvent _releaseEvent;
    private Vector3 _startPos;
    private Quaternion _startRotation;
    private Rigidbody _rb;
    private void Awake()
    {
        transform.SetParent(null, true);
        _startPos = transform.position;
        _startRotation = transform.rotation;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() => AddListener();

    private void OnDisable() => RemoveListener();

    private void AddListener()
    {
        _interactable.firstSelectEntered.AddListener(GameStarted);
        _rb.isKinematic = true;
        _rb.velocity = Vector3.zero;
    }

    private void RemoveListener()
    {
        _interactable.firstSelectEntered.RemoveListener(GameStarted);
        _rb.isKinematic = false;
    }

    private void GameStarted(SelectEnterEventArgs arg0) => GameStarted();

    public override void GameStarted()
    {
        base.GameStarted();
        RemoveListener();
    }

    public override void GameEnded()
    {
        gameObject.SetActive(false);
        _releaseEvent.Raise();
        AddListener();
        transform.SetPositionAndRotation(_startPos, _startRotation);   
        base.GameEnded();
        gameObject.SetActive(true);
    }
}
