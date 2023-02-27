using UnityEngine;

public class NudgePosition : MonoBehaviour
{
    [Tooltip("The amount of nudge is between negative and positive")]
    [SerializeField] private Vector3 _nudgeAmountRange;

    private void Start()
    {
        float x, y, z;
        x = Random.Range(-_nudgeAmountRange.x, _nudgeAmountRange.x);
        y = Random.Range(-_nudgeAmountRange.y, _nudgeAmountRange.y);
        z = Random.Range(-_nudgeAmountRange.z, _nudgeAmountRange.z);
        Vector3 nudge = new(x, y, z);
        transform.position += nudge;
    }
}
