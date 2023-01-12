using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace SkeeBall
{
    public class VrBall : Ball
    {
        [SerializeField] private XRGrabInteractable _interactable;

        protected override void OnEnable()
        {
            base.OnEnable();
            _interactable.lastSelectExited.AddListener(_ => VrThrown());
            _interactable.firstSelectEntered.AddListener(_ => VrGrabbed());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _interactable.lastSelectExited.RemoveListener(_ => VrThrown());
            _interactable.firstSelectEntered.RemoveListener(_ => VrGrabbed());
        }
        private void VrThrown() => _thrown = true;

        private void VrGrabbed()
        {
            _thrown = false;
            _timerToDisappear.StopAndReset();
        }
    }
}
