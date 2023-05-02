using UnityEngine;
using System.Collections;
using ProjectY;

namespace Shooter
{
    public class TargetFlipper : Mover
    {
        [Header("Score")]
        [SerializeField] private TargetScore _targetScore;

        public TargetScore TargetScore => _targetScore;

        //Bad hard coded but yeah
        private readonly Quaternion _laying = Quaternion.Euler(-45, 0, 180);
        private readonly Quaternion _standing = Quaternion.Euler(-180, 0, 180);

        private readonly WaitForFixedUpdate _wait = new();

        private void OnEnable() => _targetScore.Shot += ReturnToPool;

        private void Start()
        {
            //_event.Raise(this);
            _addToStartingPool.Raise(this);
            Lay();
        }

        private void OnDisable() => _targetScore.Shot -= ReturnToPool;

        public void Stand() => transform.localRotation = _standing;

        public void Lay() => transform.localRotation = _laying;

        [ContextMenu("Move Up")]
        public override void Move()
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_standing, true, -1));
        }

        [ContextMenu("Move Down")]
        public void MoveDown()
        {
            MovedDown();
        }

        public override void MovedDown(float speedMultiplier = 1)
        {
            StopAllCoroutines();
            StartCoroutine(FlipCoroutine(_laying, false, speedMultiplier));
        }

        private IEnumerator FlipCoroutine(Quaternion rotation, bool canScore, float speedMultiplier = 1 ,int positive = 1)
        {
            if (canScore)
                _targetScore.SetCanScore(canScore);

            while (!MathHelper.CompareRotations(transform.localRotation, rotation, 10))
            {
                transform.Rotate(_speed * positive * speedMultiplier * Time.deltaTime * Vector3.left);
                yield return _wait;
            }

            transform.localRotation = rotation;

            if (!canScore)
                _targetScore.SetCanScore(canScore);
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