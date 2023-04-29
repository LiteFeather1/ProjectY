using UnityEngine;

public class CanGameScoreNet : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<CanScore>(out var score))
        {
            if(!score.LeftNet)
                score.ChangeManagerScore();
        }
    }
}
