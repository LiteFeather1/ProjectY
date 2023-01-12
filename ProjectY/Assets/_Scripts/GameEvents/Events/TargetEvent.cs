using UnityEngine;
using ProjectY;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New Target Event", menuName = "Game Events/Target Event")]
    public class TargetEvent : BaseGameEvent<Mover> { }
}
