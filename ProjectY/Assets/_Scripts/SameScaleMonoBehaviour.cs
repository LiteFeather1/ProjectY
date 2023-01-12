using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectY
{
    public class SameScaleMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private SameScale _sameScale;

        void Start()
        {
            _sameScale.Setup();
        }

        // Update is called once per frame
        void Update()
        {
            _sameScale.UpdateScale();
        }
    }
}
