using UnityEngine;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New String Event", menuName = "Game Events/String Event")]
    public class StringEvent : BaseGameEvent<string> { }
}
