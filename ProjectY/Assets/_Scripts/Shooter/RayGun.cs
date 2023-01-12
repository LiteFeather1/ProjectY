using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Shooter
{
    public class RayGun : BaseGun
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _lineRayMasks;
        private Vector3 _maxLineLength;

        protected override Ray Ray => _ray.SetOriginAndDirection(transform.position, transform.forward);

        private void Start()
        {
            XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
            grabbable.activated.AddListener(_ => Shoot());
        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _range, _lineRayMasks))
            {
                if (hit.collider)
                {
                    _maxLineLength.z = hit.point.z;
                }
                else
                {
                    _maxLineLength.z = _range;
                }
            }
            _lineRenderer.SetPosition(1, _maxLineLength);
        }
    }
}
