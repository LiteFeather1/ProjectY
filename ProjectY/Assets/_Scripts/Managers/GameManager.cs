using UnityEngine;
using ScriptableObjectEvents;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VoidEvent _startGame;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
