using System.Text;
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
        StringBuilder sb = new();
        sb.Append("Time Left : ").Append(Mathf.Round(time).ToString("00"));
        _score.text = sb.ToString();
    }
}
