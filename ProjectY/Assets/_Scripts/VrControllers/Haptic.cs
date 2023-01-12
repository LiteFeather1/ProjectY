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
