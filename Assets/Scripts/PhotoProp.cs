using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoProp : MonoBehaviour
{
    public float randomForce;
    public float randomTorque;
    public SpriteRenderer spriteRenderer;

    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Sprite s)
    {
        spriteRenderer.sprite = s;

        _rigidbody.AddForce(Random.insideUnitCircle*randomForce);
        _rigidbody.AddTorque(Random.Range(-randomTorque, randomTorque));
    }
}
