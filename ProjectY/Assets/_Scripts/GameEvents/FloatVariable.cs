using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "Scriptable Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
#if UNITY_EDITOR
#pragma warning disable
    [TextArea()][SerializeField] private string _devDescription = "";
#pragma warning restore
#endif
    [SerializeField] private float _value;

    public float Value => _value;

    public void SetValue(float value) => _value = value;

    public void SetValue(FloatVariable value) => _value = value._value;

    public void ApplyChange(float amount) => _value += amount;

    public void ApplyChange(FloatVariable amount) => _value += amount._value;

    public static implicit operator float(FloatVariable floatVariable) => floatVariable._value;
}
