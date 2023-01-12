using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;
namespace WhackAMole
{
    public class WhackAMoleMover : Mover
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] Vector3 upPosition;
        [SerializeField] Vector3 downPosition;
        [SerializeField] bool shouldBeUp = true;
        [SerializeField] FloatVariable _time;
        [SerializeField] FloatVariable _elapsedTime;
        public Rigidbody body;
        public BaseScore score;
        public TargetEvent target;
        private Vector3 UpPosition => upPosition + downPosition;

        private void Start()
        {
            downPosition = transform.position;
            body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float speed = Mathf.Lerp(_speed, _maxSpeed, _elapsedTime / _time);
            body.velocity = Vector3.zero;
            if (shouldBeUp == true)
            {
                if (Vector3.Distance(transform.position, UpPosition) >= 0.1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, UpPosition, Time.deltaTime * speed);
                }
                if (Vector3.Distance(transform.position, downPosition) <= 0.1)
                {
                    MoveBack();
                }
            }
        }

        public override void Move()
        {
            shouldBeUp = false;
        }

        public override void MoveBack(float speedMultiplier = 1)
        {
            shouldBeUp = false;
            //score.ChangeManagerScore();
            //target.Raise(this);
        }

        //  O que fazer - Manager para voltar a meter as moles para cima, mudar o speed de que eles se movem com o tempo.
        // Mudar a velocidade inicial de quando elvas vao para cima


        //IEnumerator Move_Co(Vector3 startPos, Vector3 endPos)
        //{
        //    while(Vector3.Distance(transform.position,pos) <= 0.1)
        //    {
        //        transform.position = Vector3.Lerp(pos,);
        //        yield return _wait;
        //    }

        //}

    }
}