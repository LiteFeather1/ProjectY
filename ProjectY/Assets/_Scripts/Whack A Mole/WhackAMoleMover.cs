using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace WhackAMole
{
    public class WhackAMoleMover : Mover
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private Vector3 upPosition;
        private Vector3 _realUpPosition;
        [SerializeField] private Vector3 _startPos;
        private bool _up = false;
        private bool _shouldBeDown;
        private bool _wasUp;
        private bool _wasDown;
        [SerializeField] private float _threshHold = .25f;
        private float _yThreshHold;

        [Header("Components")] 
        [SerializeField] private FloatVariable _time;
        [SerializeField] private FloatVariable _elapsedTime;
        [SerializeField] private FloatEvent _addTime;

        [Header("Components")] 
        [SerializeField] private Rigidbody _body;
        [SerializeField] private WhackMoleScore _score;

        private void Start()
        {
            _realUpPosition = transform.position + upPosition;
            _startPos = transform.position;
            _event.Raise(this);
            _yThreshHold = _startPos.y + (_realUpPosition.y - _startPos.y) * _threshHold;
        }

        private void FixedUpdate()
        {
            float speed = Mathf.Lerp(_speed, _maxSpeed, _elapsedTime / _time);

            bool isDownEnough = transform.position.y <= _yThreshHold && _wasUp;
            print(isDownEnough);

            if (_shouldBeDown)
            {
                DownMovement(speed);
            }
            else if (_up)
            {
                if(isDownEnough)
                {
                    DownMovement(speed);
                    _shouldBeDown = true;
                    _score.ChangeManagerScore();
                    _addTime.Raise(1f);
                    _wasDown = false;
                }
                else
                {
                    if (Vector3.Distance(transform.position, _realUpPosition) >= 0.01f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, _realUpPosition, Time.deltaTime * speed);
                    }
                }
            }

            _wasUp = transform.position.y >= _yThreshHold;

            Limit();
        }

        private void DownMovement(float speed)
        {
            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance >= .01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPos, Time.deltaTime * speed * _backSpeedMultiplier);
            }
            else
            {
                if (_wasDown)
                    return;
                _wasDown = true;
                _event.Raise(this);
            }
            _body.velocity = Vector3.zero;
        }

        private void Limit()
        {
            if(transform.position.y > _realUpPosition.y)
                transform.position = _realUpPosition;

            if (transform.position.y < _startPos.y)
                transform.position = _startPos;
        }

        public override void Move()
        {
            _up = true;
            _shouldBeDown = false;
        }

        public void MoveDown()
        {
            MovedDown(1f);
        }

        public override void MovedDown(float speedMultiplier = 1)
        {
            if (!_up)
                return;

            _up = false;
            _shouldBeDown = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (Application.isPlaying)
                Gizmos.DrawCube(_realUpPosition, Vector3.one / 10);
            else
                Gizmos.DrawCube(transform.position + _realUpPosition, Vector3.one / 10);

            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(_startPos.x, _yThreshHold, _startPos.z), Vector3.one / 10);
        }
    }
}