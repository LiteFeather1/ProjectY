using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPusher : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float force;
    [SerializeField] private Collider _collider;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigid = collision.rigidbody;

        if (rigid != null)
        {
            rigid.AddForce(direction.normalized * force, ForceMode.VelocityChange);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_collider.bounds.center, Vector3.one/10);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_collider.bounds.center + direction, Vector3.one/10);
    }
}
