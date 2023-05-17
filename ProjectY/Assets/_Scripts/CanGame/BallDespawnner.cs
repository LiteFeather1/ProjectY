using SkeeBall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDespawnner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out VrBall ball))
            ball.VrThrown();
    }
}
