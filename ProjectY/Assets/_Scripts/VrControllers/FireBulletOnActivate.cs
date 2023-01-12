using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireSpeed;

    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs args)
    {
        var newBullet = Instantiate(_bulletPrefab);
        newBullet.transform.position = _firePoint.position;
        newBullet.velocity = _firePoint.forward * _fireSpeed;
        Destroy(newBullet.gameObject, 5f);
    }

}

