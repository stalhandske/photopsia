using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float angularVelocity;

    void Update()
    {
        transform.Rotate(0, 0, angularVelocity * Time.deltaTime);
    }

    void HandleStartLevel()
    {
        transform.localRotation = Quaternion.identity;
    }
}
