using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty _pinchAnimationAction;
    [SerializeField] private InputActionProperty _gripAnimationAction;
    [SerializeField] private Animator _handAC;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = _pinchAnimationAction.action.ReadValue<float>();
        _handAC.SetFloat("Trigger", triggerValue);
        float gripValue = _gripAnimationAction.action.ReadValue<float>();
        _handAC.SetFloat("Grip", gripValue);

    }
}
