using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPusher : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float force;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigid = collision.rigidbody;

        if (rigid != null)
        {
            rigid.AddForce(direction * force, ForceMode.VelocityChange);
        }
    }
}
