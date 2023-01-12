using UnityEngine;
using ProjectY;

namespace ScriptableObjectEvents
{
    [CreateAssetMenu(fileName = "New Feedback particle Event", menuName = "Game Events/Feedback particle Event")]
    public class FeedBackParticleEvent : BaseGameEvent<ProjectY.FeedBackParticleData> { }
}
