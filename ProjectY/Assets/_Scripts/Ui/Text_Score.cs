using UnityEngine;
using System.Text;

public class Text_Score : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _score;
    [SerializeField] private string _scoreString = "Score : ";

    //Event listener
    public void UpdateScore(float score)
    {
        StringBuilder sb = new();
        sb.Append(_scoreString).Append(Mathf.Round(score).ToString("0000"));
        _score.text = sb.ToString();
    }
}