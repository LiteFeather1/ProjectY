using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinHover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _amplitude = 16f;

    private float _startOffset;
    private float _startY;

    private void Awake()
    {
        _startY = transform.position.y;
        _startOffset = Random.value;
    }

    void Update()
    {
        float y = _startY + Mathf.Sin(_startOffset + Time.time * _speed) * _amplitude;

        transform.position = new(transform.position.x, y, transform.position.z);
    }
}
