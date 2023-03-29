using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    [Header("Left")]
    [SerializeField] private GameObject _leftTeleportation;
    [SerializeField] private InputActionProperty _leftActivate;
    [SerializeField] private InputActionProperty _leftCancel;
    [SerializeField] private XRRayInteractor _leftRay;

    [Header("Right")]
    [SerializeField] private GameObject _rightTeleportation;
    [SerializeField] private InputActionProperty _rightActivate;
    [SerializeField] private InputActionProperty _rightCancel;
    [SerializeField] private XRRayInteractor _rightRay;

    private readonly float _sensibility = 0.1f;

    void Update()
    {

        bool pressingLeftTrigger = _leftActivate.action.ReadValue<float>() > _sensibility;
        bool pressingLeftGrip = _leftCancel.action.ReadValue<float>() == 0;
        bool isLeftRayHovering = _leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNum, out bool leftValid);
        _leftTeleportation.SetActive(pressingLeftGrip && pressingLeftTrigger && !isLeftRayHovering);   
        
        bool pressingRightTrigger = _rightActivate.action.ReadValue<float>() > _sensibility;
        bool pressingRightGrip = _rightCancel.action.ReadValue<float>() == 0;
        bool isRightRayHovering = _leftRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNum, out bool rightValid);
        _rightTeleportation.SetActive(pressingRightTrigger && pressingRightGrip && !isRightRayHovering);        
    }
}
