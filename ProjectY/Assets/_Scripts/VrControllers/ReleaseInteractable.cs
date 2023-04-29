using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ReleaseInteractable : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _interactor;

    // Event
    public void Release()
    {
        _interactor.EndManualInteraction();

        _interactor.EndManualInteraction();

    }
}
