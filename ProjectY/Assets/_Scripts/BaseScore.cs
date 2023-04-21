using UnityEngine;
using ScriptableObjectEvents;

public class BaseScore : MonoBehaviour
{
    [Header("Base Score")]
    [SerializeField] protected FloatEvent _scored;
    [SerializeField] protected float _baseScore;
    protected bool _canScore = true;
    public float Score => _baseScore;
    public void SetCanScore(bool canScore) => _canScore = canScore;

    public void SetScore(float score) => _baseScore = score;

    public System.Action Scored { get; set; }

    public virtual void ChangeManagerScore()
    {
        if (!_canScore)
            return;

        _scored.Raise(_baseScore);
        Scored?.Invoke();
    }
}