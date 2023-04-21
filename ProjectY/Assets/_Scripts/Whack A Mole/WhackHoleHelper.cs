using UnityEngine;

namespace WhackAMole
{
    public class WhackHoleHelper : MonoBehaviour
    {
        [SerializeField] private Transform _where;
        [SerializeField] private float _force = 200f;

        private void OnTriggerStay(Collider other)
        {
            Rigidbody otherBody = other.attachedRigidbody;

            if (otherBody == null)
                return;

            //otherBody.velocity = Vector3.zero;

            Vector3 direction = other.transform.position - _where.position;
            direction.Normalize();
            direction *= -1;

            otherBody.AddForce(direction * _force, ForceMode.VelocityChange);

            //otherBody.velocity = direction * _force;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_where.position, .16f/4);
        }
    }
}