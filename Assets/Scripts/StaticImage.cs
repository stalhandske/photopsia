using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StaticImage : MonoBehaviour
{
    public float timeToDevelopePic;

    public Vector2 forceToAdd;
    public float torqueToAdd;

    public Image overlayImage;
    public RawImage snapImage;

    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Texture2D t2d)
    {
        snapImage.texture = t2d;

        Color c = overlayImage.color;
        c.a = 1;
        overlayImage.color = c;
        overlayImage.DOFade(0, timeToDevelopePic);

        _rigidbody.AddForce(forceToAdd * Random.Range(1f, 3f));
        _rigidbody.AddTorque(torqueToAdd * Random.Range(-3f, 3f));

        StartCoroutine(ShatterPauseCr());
    }

    IEnumerator ShatterPauseCr()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
