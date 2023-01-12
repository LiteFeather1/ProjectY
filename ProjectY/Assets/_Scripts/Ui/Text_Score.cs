using UnityEngine;
using System.Text;

public class Text_Score : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _score;

    //Event listener
    public void UpdateScore(float score)
    {
        StringBuilder sb = new();
        sb.Append("Score : ").Append(Mathf.Round(score).ToString("000000"));
        _score.text = sb.ToString();
    }
}