using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace WhackAMole
{
    public class WhackAMoleMover : Mover
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] Vector3 upPosition;
        [SerializeField] Vector3 _startPos;
        [SerializeField] FloatVariable _time;
        [SerializeField] FloatVariable _elapsedTime;
        private bool _up = false;
        public Rigidbody body;
        public BaseScore score;
        public TargetEvent target;
        private Vector3 UpPosition => _startPos + upPosition;

        private void Start()
        {
            _startPos = transform.position;
            body = GetComponent<Rigidbody>();
            _event.Raise(this);
        }

        private void Update()
        {
            float speed = Mathf.Lerp(_speed, _maxSpeed, _elapsedTime / _time);
            body.velocity = Vector3.zero;

            if (_up)
            {
                if (Vector3.Distance(transform.position, UpPosition) >= 0.1)
                {
                    print("moving");
                    transform.position = Vector3.MoveTowards(transform.position, UpPosition, Time.deltaTime * speed);
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, _startPos) <= 0.1)
                {
                    MovedDown();
                }
            }
            Limit();
        }

        private void Limit()
        {
            if(transform.position.y > UpPosition.y)
                transform.position = UpPosition;

            if (transform.position.y < _startPos.y)
                transform.position = _startPos;
        }

        public override void Move()
        {
            _up = true;
        }

        public override void MovedDown(float speedMultiplier = 1)
        {
            if (!_up)
                return;

            _up = false;
            score.ChangeManagerScore();
            target.Raise(this);
        }

        //  O que fazer - Manager para voltar a meter as moles para cima, mudar o speed de que eles se movem com o tempo.
        // Mudar a velocidade inicial de quando elvas vao para cima

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (Application.isPlaying)
                Gizmos.DrawCube(UpPosition, Vector3.one / 10);
            else
                Gizmos.DrawCube(transform.position + upPosition, Vector3.one / 10);

        }
    }
}