using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGameScoreNet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if(other.TryGetComponent<BaseScore>(out var score))
        {
            print("yrs");
            score.ChangeManagerScore();
        }
        else
        {
            print("NOpe");
        }
    }
}
