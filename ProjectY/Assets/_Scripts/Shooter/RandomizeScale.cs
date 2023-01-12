using UnityEngine;

public class RandomizeScale : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector2 _scaleRange;

    private void Awake()
    {
        float r = Random.Range(_scaleRange.x, _scaleRange.y);
        _transform.localScale = Vector3.one * r;
    }
}
