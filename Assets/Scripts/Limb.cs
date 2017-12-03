using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Limb : MonoBehaviour {

    public System.Action DidUpdate;

    public float maxDist = .1f;

    public float randAmount;

    public float moveLerp = 0.6f;

    public Vector2 localStartPosition;

    Vector2 targetWorldPosition;
    Vector2 worldPosition;

    float acutalMaxDist;

    AudioSource _audioSource;

    void Awake()
    {
        //localStartPosition = transform.localPosition;
        SetLocalPosition(localStartPosition);// + Random.insideUnitCircle*maxDist);
        _audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        
        transform.position = Vector2.Lerp(worldPosition, targetWorldPosition,moveLerp);
        worldPosition = transform.position;

        Vector2 direction = localStartPosition - (Vector2)transform.localPosition;
        float distance = direction.magnitude;
        if (distance > acutalMaxDist)
        {
            SetLocalPosition(localStartPosition + direction * .5f + Random.insideUnitCircle * maxDist * randAmount);
        }
        else if (Random.value > 0.99f)
        {
            SetLocalPosition(localStartPosition + Random.insideUnitCircle * maxDist * randAmount);
        }
        

        if (DidUpdate != null)
            DidUpdate();
    }

    void SetLocalPosition(Vector2 newLocalPos)
    {
        targetWorldPosition = (Vector2) transform.parent.position + newLocalPos;
        //targetWorldPosition = transform.localToWorldMatrix * newLocalPos;
        acutalMaxDist = maxDist + Random.Range(-.2f, .2f);
        if (_audioSource)
        {
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.Play();
        }
    }

}
