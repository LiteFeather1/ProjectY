using ScriptableObjectEvents;
using UnityEngine;

public class CanScore : BaseScore
{
    [SerializeField] private VoidEvent _knocked;
    private bool _leftNet = false;

    public bool LeftNet => _leftNet;

    public override void ChangeManagerScore()
    {
        base.ChangeManagerScore();
        _knocked.Raise();
        _leftNet = true;    
    }
}
