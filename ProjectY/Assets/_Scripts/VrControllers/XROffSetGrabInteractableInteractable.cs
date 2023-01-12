using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffSetGrabInteractableInteractable : XRGrabInteractable
{
    private Vector3 _initialLocalPos;
    private Quaternion _initialLocalRot;

    private void Start()
    {
        if(!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offset Grab Pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }
        else
        {
            _initialLocalPos = attachTransform.localPosition;
            _initialLocalRot = attachTransform.localRotation;
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRDirectInteractor)
        {
            attachTransform.SetPositionAndRotation(args.interactorObject.transform.position, args.interactorObject.transform.rotation);
        }
        else
        {
            attachTransform.SetPositionAndRotation(_initialLocalPos, _initialLocalRot);
        }
        base.OnSelectEntered(args);
    }
}
