using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    [SerializeField] private float _intensity;
    [SerializeField] private float _duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if(eventArgs.interactableObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    private void TriggerHaptic(XRBaseController controller)
    {
        if (_intensity > 0)
            controller.SendHapticImpulse(_intensity, _duration);
    }
}

public class HapticInteractable : MonoBehaviour
{
    [SerializeField] private Haptic _hapticOnActivated;
    [SerializeField] private Haptic _hapticHoverEntered;
    [SerializeField] private Haptic _hapticHoverExited;
    [SerializeField] private Haptic _hapticSelectEntered;
    [SerializeField] private Haptic _hapticSelectExit;

    [SerializeField] private XRBaseInteractable _interactable;

    private void Start()
    {
        _interactable.activated.AddListener(_hapticOnActivated.TriggerHaptic);
        _interactable.hoverEntered.AddListener(_hapticHoverEntered.TriggerHaptic);
        _interactable.hoverExited.AddListener(_hapticHoverExited.TriggerHaptic);
        _interactable.selectEntered.AddListener(_hapticSelectEntered.TriggerHaptic);
        _interactable.selectExited.AddListener(_hapticSelectExit.TriggerHaptic);
    }
}
