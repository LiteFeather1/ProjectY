using UnityEngine;

public class ActivateWhenGame : MonoBehaviour
{
    [SerializeField] private Games _gameToListen;

    //Event
    public void ListenToGameBegin(Games games)
    {
        if (_gameToListen != games)
            return;
    }
}
