using System.Text;
using System.Collections;
using UnityEngine;
using TMPro;

namespace ProjectY
{
    public class FeedBackPopUpScore : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private TextMeshPro _popUpText;
        private readonly StringBuilder _sb = new();

        [Header("Animation")]
        [SerializeField] private float _timeToDoAnimation;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private Vector3 _animationEndPos;
        [SerializeField] private Gradient _animationColour;
        private float _startTextSize;
        [SerializeField] private float _endTextSize;
        private readonly WaitForEndOfFrame _wait = new();
        private Camera _cam;
        [SerializeField] private SameScale _scaleIndepentOfDistance;

        private void Start()
        {
            _cam = Camera.main;
            _startTextSize = _popUpText.fontSize;
            _scaleIndepentOfDistance.Setup();
        }

        private IEnumerator Animation()
        {
            float e = 0;
            Vector3 animationStartPos = transform.position + _offset;
            _popUpText.color = _animationColour.colorKeys[0].color;
            while (e < _timeToDoAnimation)
            {
                e += Time.deltaTime;
                float t = _animationCurve.Evaluate(e / _timeToDoAnimation);
                _popUpText.transform.position = Vector3.Lerp(animationStartPos, _animationEndPos + animationStartPos, t);
                _popUpText.color = _animationColour.Evaluate(t);
                _popUpText.fontSize = Mathf.Lerp(_startTextSize, _endTextSize, t);
                _scaleIndepentOfDistance.UpdateScale();
                yield return _wait;
            }
            _popUpText.enabled = false;
        }

        //Event listener
        public void SetPosition(Vector3 pos)
        {
            transform.position = pos + _offset;
        }

        //Event Listener
        public void SetText(float score)
        {
            _popUpText.text = ScoreText(score);
            _scaleIndepentOfDistance.UpdateScale();
            Vector3 pos = transform.position - _cam.transform.position;
            transform.rotation = Quaternion.LookRotation(pos);
            StopAllCoroutines();
            _popUpText.enabled = true;
            StartCoroutine(Animation());
        }

        private string ScoreText(float score)
        {
            _sb.Clear();
            _sb.Append(score > 0 ? "+" : "").Append(Mathf.RoundToInt(score).ToString());
            return _sb.ToString();
        }
    }
}

