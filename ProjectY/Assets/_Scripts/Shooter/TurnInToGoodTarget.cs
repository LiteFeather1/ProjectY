using UnityEngine;
using ProjectY;

namespace Shooter
{
    public class TurnInToGoodTarget : MonoBehaviour
    {
        [Range(0, 1f)][SerializeField] private float _chance;

        [Header("Target")]
        [SerializeField] private TargetFlipper _flipper;

        [Header("Visuals")]
        [SerializeField] private Material _goodMaterial;
        [SerializeField] private Renderer _renderer;

        private void Awake()
        {
            if (_chance > Random.value)
            {
                _renderer.material = _goodMaterial;
                _flipper.SetType(TargetType.Good);
            }
        }
    }
}
