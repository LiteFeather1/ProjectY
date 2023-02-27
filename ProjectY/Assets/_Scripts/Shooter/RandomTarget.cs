using UnityEngine;

namespace Shooter
{
    public class RandomTarget : MonoBehaviour
    {
        [SerializeField] private TargetFlipper[] _possibleTargets;
        [SerializeField] private FloatVariable _laneMultipler;

        private void Awake()
        {
            int r = Random.Range(0, _possibleTargets.Length);
            var target = Instantiate(_possibleTargets[r], transform.position, Quaternion.identity, transform.parent);
            target.TargetScore.SetLaneMultiplier(_laneMultipler);
            gameObject.SetActive(false);
        }
    }
}