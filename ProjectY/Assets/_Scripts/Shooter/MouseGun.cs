using UnityEngine;

namespace Shooter
{
    public class MouseGun : BaseGun
    {
        private bool _shoot;
        [SerializeField] private Camera _cam;
        protected override Ray Ray => _ray;

        private void Start() => _ray.origin = _cam.transform.position;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _ray.direction = _cam.transform.forward;
                _shoot = true;
            }
        }

        private void FixedUpdate()
        {
            if(_shoot)
            {
                _shoot = false;
                Shoot();
            }
        }
    }
}
