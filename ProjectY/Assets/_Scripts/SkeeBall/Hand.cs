using Shooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkeeBall
{
    class Hand : MonoBehaviour
    {
        [SerializeField] private float _grabRange;
        [SerializeField] private Ball _ball;
        [SerializeField] private float _force;
        [SerializeField] private Camera _cam;
        private Vector3 _hitPoint;
        Ray ray;

        private void Update()
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_ball == null)
                    return;
                Thrown();
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (_ball != null)
                    return;
                PickUpBall();
            }
        }

        private void Thrown()
        {
            _ball.Thrown(_cam.transform.forward, _force);
            _ball = null;
        }

        private void PickUpBall()
        {
            ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _grabRange))
            {
                if (hit.transform.TryGetComponent(out Ball ball))
                {
                    _ball = ball.PickUp(transform, _cam.transform);
                }
                _hitPoint = hit.point;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_cam.transform.position, _hitPoint.normalized * _grabRange);
        }
    }
}  
