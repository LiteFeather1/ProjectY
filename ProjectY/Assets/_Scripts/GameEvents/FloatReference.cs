using UnityEngine;
[System.Serializable]
public class FloatReference
{
    [SerializeField] private bool _useConstant = true;
    [SerializeField] private float _constantValue;
    [SerializeField] private FloatVariable _varialbe;

    public FloatReference()
    { }

    public FloatReference(float value)
    {
        _useConstant = true;
        _constantValue = value;
    }

    public float Value => _useConstant ? _constantValue : _varialbe.Value;

    public static implicit operator float(FloatReference reference) => reference.Value;
}
