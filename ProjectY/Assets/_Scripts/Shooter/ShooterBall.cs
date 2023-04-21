using ProjectY;
using UnityEngine;

namespace Shooter
{
    public class ShooterBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Timer _despawnTimer;

        private void OnEnable()
        {
            _despawnTimer.TimeEvent += () => { Destroy(gameObject); };
        }

        private void OnDisable()
        {
            _despawnTimer.TimeEvent -= () => { Destroy(gameObject); };
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out TargetScore targetable))
            {
                targetable.ChangeManagerScore(collision.contacts[0].point);
            }

            _rb.velocity = Vector3.zero;
            _rb.constraints = RigidbodyConstraints.FreezePosition;
            _rb.isKinematic = true;
            transform.SetParent(collision.transform);
        }
    }
}
