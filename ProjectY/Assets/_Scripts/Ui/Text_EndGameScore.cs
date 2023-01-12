using UnityEngine;

public class Text_EndGameScore : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _score;
    [SerializeField] private Games _game;

    private void Start()
    {
        _score.text = PlayerPrefs.GetFloat(_game.ToString()).ToString("000000");
    }
}
