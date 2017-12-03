using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public float radius;
    public float time;

    float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > time)
        {
            _timer = 0;
            Instantiate(prefab, transform.position + (Vector3)Random.insideUnitCircle.normalized * radius, Quaternion.identity);
        }
    }

}
