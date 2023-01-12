using UnityEngine;

[System.Serializable]
public class SameScale
{
    [SerializeField] private Transform _objectToScale;
    [SerializeField] private Camera _cam;
    [SerializeField] private float _scaleFactor = 1;

    private Vector3 _initialScale;
    private float _initialDistance;

    public void Setup()
    { 
        _initialScale = _objectToScale.localScale;
        _initialDistance = Vector3.Distance(_objectToScale.position, _cam.transform.position);
    }

    public void UpdateScale()
    {
        float distance = Vector3.Distance(_objectToScale.position, _cam.transform.position);
        float scaleFactor = _initialScale.x * _scaleFactor * distance / _initialDistance;
        _objectToScale.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}