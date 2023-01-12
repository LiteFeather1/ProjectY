using UnityEngine;
using System.Collections;
using ProjectY;

namespace Shooter
{
    public class TargetFlipper : Mover
    {
        [Header("Score")]
        [SerializeField] private TargetScore _targetScore;

        //Bad hardCoded but yeah
        private readonly Quaternion _laying = Quaternion.Euler(100, 0, 0);
        private readonly Quaternion _standing = Quaternion.Euler(0, 0, 0);

        private readonly WaitForEndOfFrame _wait = new();

        private void OnEnable() => _targetScore.Shot += ReturnToPool;

        private void OnDisable() => _targetScore.Shot -= ReturnToPool;

        public void Stand() => transform.rotation = _standing;
        public void Lay() => transform.rotation = _laying;

        public override void Move()
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_standing, -1));
        }

        public override void MoveBack(float speedMultiplier = 1)
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_laying, speedMultiplier));
        }

        private IEnumerator FlipCoroutine(Quaternion rotation, float speedMultiplier = 1 ,int positive = 1)
        {
            while (!MathHelper.CompareRotations(transform.rotation, rotation, 10))
            {
                transform.Rotate(_speed * positive * speedMultiplier * Time.deltaTime * Vector3.right);
                yield return _wait;
            }
            transform.rotation = rotation;
        }

        public void SetType(TargetType type)
        {
            _badOrGood = type;
            if (type == TargetType.Good)
                _targetScore.SetScore(-_targetScore.Score);
            else
                _targetScore.SetScore(Mathf.Abs(_targetScore.Score));
        }
    }
}