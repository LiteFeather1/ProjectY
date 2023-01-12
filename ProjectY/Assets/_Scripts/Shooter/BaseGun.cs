using UnityEngine;

namespace Shooter
{
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] protected float _range = 20;
        private Vector3 _hitPoint;
        [SerializeField] private LayerMask _targetLayer;
        protected Ray _ray;
        protected abstract Ray Ray { get; }

        protected void Shoot()
        {
            if (Physics.Raycast(Ray, out RaycastHit hit, _range, _targetLayer))
            {
                if (hit.transform.TryGetComponent(out TargetScore targetable))
                {
                    targetable.ChangeManagerScore(hit.point);
                }
                _hitPoint = hit.point;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _hitPoint);
        }
    }
}
