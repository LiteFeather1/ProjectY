using UnityEngine;
using ScriptableObjectEvents;
using ProjectY;

namespace SkeeBall
{
    public class Ball : MonoBehaviour
    {
        [Header("Thorwn")]
        [SerializeField] private BallEvent _thrownEvent;
        protected bool _thrown;
        private bool _scored;
        public bool Scored => _scored;

        [Header("Velocity")]
        [SerializeField] private Rigidbody _rb;
        [Tooltip("The event triggers when the rigidibody speed is under this value")]
        [SerializeField] private float _velocityTolerance = 1f;
        private bool CheckIfMoving => _rb.velocity.magnitude > _velocityTolerance;
        [SerializeField] protected Timer _timerToDisappear;

        protected virtual void OnEnable()
        {
            _timerToDisappear.TimeEvent += Disappear;
        }

        protected virtual void OnDisable()
        {
            _timerToDisappear.TimeEvent -= Disappear;
        }

        private void LateUpdate()
        {
            if (!_thrown && !_scored)
                return;
            if(CheckIfMoving)
            {
                _timerToDisappear.Stop();
            }
            else
            {
                _timerToDisappear.Continue();
            }
        }

        public void Appear(Transform pos)
        {
            transform.position = pos.position;
            gameObject.SetActive(true);
            enabled = true;
            _scored = false;
            _thrown = false;
        }

        public void Disappear()
        {
            _thrownEvent.Raise(this);
            gameObject.SetActive(false);
            _timerToDisappear.StopAndReset();
            _rb.velocity = Vector3.zero;
        }

        public void EnteredAHole()
        {
            _scored = true;
            _timerToDisappear.Continue();
        }  

        public void Thrown(Vector3 direction, float force)
        {
            transform.SetParent(null);
            _rb.isKinematic = false;
            //Add force takes 2 frames to add?????????????????????????????????
            _rb.velocity = direction * force;
            _rb.useGravity = true;
            _thrown = true;
        }

        public Ball PickUp(Transform point, Transform parent)
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            transform.SetParent(parent);
            transform.SetPositionAndRotation(point.position, point.rotation);
            return this;
        }
    }
}
