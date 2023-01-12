using SkeeBall;
using UnityEngine;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New Ball Event", menuName = "Game Events/Ball Event")]
    public class BallEvent : BaseGameEvent<Ball> { }
}
