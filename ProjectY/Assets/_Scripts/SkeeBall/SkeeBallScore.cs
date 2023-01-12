using UnityEngine;

namespace SkeeBall
{
    public class SkeeBallScore : BaseScore
    {
        public System.Action Scored { get; set; }

        [Header("FeedBack")]
        [SerializeField] private ScriptableObjectEvents.StringEvent _feedBackMessage;

        protected virtual string FeedBackMessage { get => $"{_baseScore}"; }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Ball ball))
            {
                if (ball.Scored)
                    return;
                _feedBackMessage?.Raise(FeedBackMessage);
                ChangeManagerScore();
                ball.EnteredAHole();
                Scored?.Invoke();
            }
        }
    }
}
