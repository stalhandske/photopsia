using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public Transform target;
    public float followTargetAmount = 0.1f;

    Vector3 _startPos;

    void Awake()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        transform.position = _startPos + followTargetAmount * target.position;
    }
}
