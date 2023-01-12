using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    [SerializeField] private GameObject _leftGrabRay;
    [SerializeField] private XRDirectInteractor _leftDirectGrab;
    [SerializeField] private GameObject _rightGrabRay;
    [SerializeField] private XRDirectInteractor _rightDirectGrab;

    private void Update()
    {
        _leftGrabRay.SetActive(_leftDirectGrab.interactablesSelected.Count == 0);
        _rightGrabRay.SetActive(_rightDirectGrab.interactablesSelected.Count == 0);
    }
}
