using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole
{
    public class WhackMoleScore : BaseScore
    {

        private void OnTriggerEnter(Collider other)
        {
            ChangeManagerScore();
        }
    }
}