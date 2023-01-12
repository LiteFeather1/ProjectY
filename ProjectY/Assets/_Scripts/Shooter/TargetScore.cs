using UnityEngine;
using ProjectY;
using ScriptableObjectEvents;

namespace Shooter
{
    public class TargetScore : BaseScore
    {
        [Header("Target Score")]
        private float _scoreToRaise;
        [SerializeField] private Transform _targetCenter;
        [Tooltip("From center to outer")]
        [SerializeField] private Transform[] _rings = new Transform[3];
        [SerializeField] private FloatVariable _laneMultiplier;
        private float TargetSize => _targetCenter.localScale.x;
        //Might need to map Target size multiplier from min size max size to min multiplier max multiplier  

        [Header("Timers")]
        [SerializeField] private ChangeTimerValue _addTime = new(1);
        [SerializeField] private ChangeTimerValue _removeTime = new(-1);
        [Space]
        [Header("Feed Back Message")]
        [SerializeField] private StringEvent _feedBackMessageEvent;
        [Tooltip("From Center message to outside message")]
        [SerializeField] private string[] _feedBackMessage = new string[4] {"Bulls eye!", "Okay!", "Bad!", "Terrible!" };

        [Header("Feed Back Particle")]
        [SerializeField] private FeedBackParticleEvent _feedBackParticleEvent;
        [SerializeField] private FeedBackParticleData[] _feedBackParticleData = new FeedBackParticleData[4];

        [Header("Feed Back Particle")]
        [SerializeField] private Vector3Event _scorePopUp;

        public System.Action Shot { get; set; }

        public override void ChangeManagerScore()
        { 
            _scorePopUp.Raise(transform.position);
            _scored.Raise(_scoreToRaise);
            Shot?.Invoke();
        }

        public void ChangeManagerScore(Vector3 bulletPoint)
        {
            // Using vector3 might be better because we doenst use the Z axis. All we should  care for is the distance on a 2d plane
            // But this means our is locked in a rotation , where Z is depth
            float distanceFromCenter = Vector2.Distance(bulletPoint, _targetCenter.position);

            //Inner ring
            if (distanceFromCenter < RingRadius(0))
            {
                _scoreToRaise = _baseScore * 2 * _laneMultiplier / TargetSize;
                _addTime.Change();
                RaiseFeedBack(0, bulletPoint);
            }
            //Inbetween ring
            else if (distanceFromCenter < RingRadius(1))
            {
                _scoreToRaise = _baseScore * _laneMultiplier / TargetSize;
                RaiseFeedBack(1, bulletPoint);
            }
            //Outer ring
            else if (distanceFromCenter < RingRadius(2))
            {
                _scoreToRaise = _baseScore / 2 * _laneMultiplier / TargetSize;
                RaiseFeedBack(2, bulletPoint);
            }
            //Too Far
            else
            {
                _scoreToRaise = -_baseScore / 2 * _laneMultiplier / TargetSize;
                _removeTime.Change();
                RaiseFeedBack(3, bulletPoint);
            }

            if (_baseScore < 0)
            {
                if (_scoreToRaise > 0)
                    _scoreToRaise = -_scoreToRaise;
                _feedBackMessageEvent.Raise("Not an enemy!");
            }

            ChangeManagerScore();
        }

        private void RaiseFeedBack(int i,Vector3 bulletPoint)
        {
            if (_baseScore < 0)
                return;
            _feedBackMessageEvent.Raise(_feedBackMessage[i]);
            FeedBackParticleData feedBackData = _feedBackParticleData[i];
            feedBackData.SetPosAndAmount(bulletPoint, (int)(_laneMultiplier * 20));
            _feedBackParticleEvent.Raise(feedBackData);
        }

        private float RingRadius(int i)
        {
            return _rings[i].localScale.x * TargetSize / 2;
        }

        //private void OnDrawGizmos()
        //{
        //    if (_targetCenter == null)
        //        return;
        //    DrawTargetRadius(RingRadius(2), Color.blue);
        //    DrawTargetRadius(RingRadius(1), Color.red);
        //    DrawTargetRadius(RingRadius(0), Color.yellow);
        //}

        //private void DrawTargetRadius(float radius, Color color)
        //{
        //    Gizmos.color = color;
        //    Gizmos.DrawSphere(_targetCenter.position, radius);
        //}
    }
}