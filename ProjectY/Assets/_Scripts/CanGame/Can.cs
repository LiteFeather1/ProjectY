using UnityEngine;

public class Can : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _centerOfMass;

    private void Awake()
    {
        //SetCenterOfMass();
    }

    public void SetCenterOfMass() => _rb.centerOfMass = _centerOfMass.localPosition;

    public void SetMaterial(Material material) => _renderer.material= material;
}
