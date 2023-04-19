using UnityEngine;

public class Text_Time : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _score;
    [SerializeField] private FloatVariable _time;

    private void Update()
    {
        UpdateText(_time.Value);
    }

    public void UpdateText(float time)
    {
        int minute = (int)(time / 60);
        int secs = (int)(time % 60);

        _score.text = "Time Left: " + string.Format("{0:00} : {1:00}", minute, secs);
    }
}
