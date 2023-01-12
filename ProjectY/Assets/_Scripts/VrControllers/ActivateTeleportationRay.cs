using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject _leftTeleportation;
    [SerializeField] private InputActionProperty _leftActivate;
    [SerializeField] private InputActionProperty _leftCancel;
    [Space ]
    [SerializeField] private GameObject _rightTeleportation;
    [SerializeField] private InputActionProperty _rightActivate;
    [SerializeField] private InputActionProperty _rightCancel;

    private readonly float _sensibility = 0.1f;


    void Update()
    {
        bool pressingLeftTrigger = _leftActivate.action.ReadValue<float>() > _sensibility;
        bool pressingLeftGrip = _leftCancel.action.ReadValue<float>() == 0;
        _leftTeleportation.SetActive(pressingLeftGrip && pressingLeftTrigger);   
        
        bool pressingRightTrigger = _rightActivate.action.ReadValue<float>() > _sensibility;
        bool pressingRightGrip = _rightCancel.action.ReadValue<float>() == 0;
        _rightTeleportation.SetActive(pressingRightTrigger && pressingRightGrip);        
    }
}
