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
        private bool _up;
        public Rigidbody body;
        public BaseScore score;
        public TargetEvent target;
        private Vector3 UpPosition => _startPos + upPosition;

        private void Start()
        {
            _startPos = transform.position;
            body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float speed = Mathf.Lerp(_speed, _maxSpeed, _elapsedTime / _time);
            body.velocity = Vector3.zero;

            if (Vector3.Distance(transform.position, UpPosition) >= 0.1)
            {
                print("moving");
                transform.position = Vector3.MoveTowards(transform.position, UpPosition, Time.deltaTime * speed);
            }

            if (Vector3.Distance(transform.position, _startPos) <= 0.1)
            {
                if (_up)
                    return;
                MovedDown();
            }
        }

        public override void Move()
        {
            print("Hello");
            enabled = true;
            _up = true;
        }

        public override void MovedDown(float speedMultiplier = 1)
        {
            if (!_up)
                return;
            _up = false;
            score.ChangeManagerScore();
            target.Raise(this);
            enabled = false;
        }

        //  O que fazer - Manager para voltar a meter as moles para cima, mudar o speed de que eles se movem com o tempo.
        // Mudar a velocidade inicial de quando elvas vao para cima

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (Application.isPlaying)
                Gizmos.DrawCube(UpPosition, Vector3.one / 3);
            else
                Gizmos.DrawCube(transform.position + upPosition, Vector3.one / 3);

        }
    }
}