using UnityEngine;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "Game Events/Bool Event")]
    public class BoolEvent : BaseGameEvent<bool> { }
}
