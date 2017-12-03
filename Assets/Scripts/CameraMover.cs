using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public AnimationCurve zoomCurve;
    public AnimationCurve followCurve;
    public Transform target;

    Vector3 _startPos;

    void Awake()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        transform.position = _startPos + followCurve.Evaluate(Time.timeSinceLevelLoad) * target.position;
        Camera.main.orthographicSize = zoomCurve.Evaluate(Time.timeSinceLevelLoad);
    }
}
