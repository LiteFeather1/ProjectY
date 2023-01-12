using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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
