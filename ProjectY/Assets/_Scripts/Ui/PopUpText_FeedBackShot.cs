using System.Collections;
using UnityEngine;

public class PopUpText_FeedBackShot : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private Gradient _fadeGradient;
    [SerializeField] private float _timeToFade = 0.25f;

    private readonly WaitForEndOfFrame _wait = new();

    //Event listener to Target feedBacktext
    public void PopUp(string message)
    {
        _text.text = message;
        StopAllCoroutines();
        StartCoroutine(PopUpCo());
    }

    private IEnumerator PopUpCo()
    {
        float eTime = 0;
        _text.color = Color.white;
        while(eTime < _timeToFade)
        {
            _text.color = _fadeGradient.Evaluate(eTime / _timeToFade);
            eTime += Time.deltaTime;
            yield return _wait;
        }
        _text.color = Color.clear;
    }
}
