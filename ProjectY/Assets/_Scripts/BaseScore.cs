using UnityEngine;
using ScriptableObjectEvents;

public class BaseScore : MonoBehaviour
{
    [Header("Base Score")]
    [SerializeField] protected FloatEvent _scored;
    [SerializeField] protected float _baseScore;

    public float Score => _baseScore;

    public void SetScore(float score) => _baseScore = score;

    public virtual void ChangeManagerScore()
    {
        _scored.Raise(_baseScore);
    }
}