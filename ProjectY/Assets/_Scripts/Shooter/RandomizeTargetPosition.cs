using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shooter
{
    public class RandomizeTargetPosition : MonoBehaviour
    {
        [SerializeField] private TargetFlipper _flipper;
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _outerRing;
        [SerializeField] private bool _showGizmos;

        private void Start() => Randomize();

        private void Randomize()
        {
            Bounds(out Vector3 bounds);
            float xOffSet = Random.Range(0, bounds.x) + _body.position.x;
            float yOffSet = Random.Range(0, bounds.y) + _body.position.y;
            Vector3 pos = new(xOffSet, yOffSet, _target.position.z);
            _target.position = pos;
            _flipper.Lay();
        }

        private void Bounds(out Vector3 bounds)
        {
            bounds = Vector3.zero;
            if (_target == null || _target == null || _outerRing == null)
                return;
            float scale = _target.localScale.x;
            float outerRingDiameter = _outerRing.localScale.x;
            float totalDiameter = scale * outerRingDiameter;

            Vector3 targetSize = new(totalDiameter, totalDiameter);
            Vector3 b = _body.localScale - targetSize;
            b.z = 0.001f;
            bounds = b / 2;
        }

        private void OnDrawGizmos()
        {
            if (!_showGizmos)
                return;
            Gizmos.color = Color.black;
            Bounds(out Vector3 bounds);
            bounds = Quaternion.Euler(transform.eulerAngles) * bounds;
            Vector3 pos = _body.position;
            pos.z = _target.position.z;
            Gizmos.DrawCube(pos, bounds * 2);
        }
    }
}