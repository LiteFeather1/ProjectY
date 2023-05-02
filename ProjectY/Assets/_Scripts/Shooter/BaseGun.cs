using UnityEngine;

namespace Shooter
{
    public abstract class BaseGun : MonoBehaviour
    {
        [SerializeField] protected float _range = 20;
        private Vector3 _hitPoint;
        [SerializeField] private LayerMask _targetLayer;

        [Header("Projectile")]
        [SerializeField] private Rigidbody _ballPrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _force = 20f;
        protected Ray _ray;
        protected abstract Ray Ray { get; }

        protected void Shoot()
        {
            //if (Physics.Raycast(Ray, out RaycastHit hit, _range, _targetLayer))
            //{
            //    if (hit.transform.TryGetComponent(out TargetScore targetable))
            //    {
            //        targetable.ChangeManagerScore(hit.point);
            //    }
            //    _hitPoint = hit.point;
            //}
            Rigidbody newProjectile = Instantiate(_ballPrefab, _firePoint.position, _firePoint.rotation);
            newProjectile.AddForce(_firePoint.forward * _force, ForceMode.VelocityChange);
            newProjectile.AddTorque(_force * 20 * Vector3.forward, ForceMode.VelocityChange);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _hitPoint);
        }
    }
}
