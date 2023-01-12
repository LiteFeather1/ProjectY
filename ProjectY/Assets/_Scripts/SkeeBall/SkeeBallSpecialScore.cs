using UnityEngine;
using ScriptableObjectEvents;

namespace SkeeBall
{
    public class SkeeBallSpecialScore : MonoBehaviour
    {
        [SerializeField] private SkeeBallScore[] _scores;
        private SkeeBallScore _currentScore;
        [SerializeField] private VoidEvent _specialScore;
        [Space]
        [SerializeField] private string _feedBackScore = "You got 1 Ball back";
        [SerializeField] private StringEvent _feedBackEvent;
        private void OnEnable()
        {
            _currentScore = _scores[0];
            ChangePos();
        }

        private void OnDisable()
        {
            _currentScore.Scored -= ChangePos;
        }

        //Event Listener. Whenever Player scores
        public void ChangePos()
        {
            _specialScore.Raise();
            _feedBackEvent.Raise(_feedBackScore);
            int i = Random.Range(0,_scores.Length);
            _currentScore.Scored -= ChangePos;
            _currentScore = _scores[i];
            transform.SetPositionAndRotation(_currentScore.transform.position, _currentScore.transform.rotation);
            _currentScore.Scored += ChangePos;;
        }
    }
}  
