using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    public static event System.Action OnSnap;

    public float frameDistance;
    public float snapTime;
    public float idleAlpha;
    public SpriteRenderer[] camSpriteRenderers;
    public RenderTexture renderTexture;

    Vector2 _target;
    SpriteRenderer[] _spriterenderers;
    bool _snapping;
    AudioSource _audioSource;

    void Awake()
    {
        _spriterenderers = GetComponentsInChildren<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

	void Update () {
	    Vector2 moveInput = new Vector2(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));

	    _target = frameDistance * moveInput;

        if (!_snapping)
    	    transform.localPosition = Vector2.Lerp(transform.localPosition, _target, 0.1f);

	    float inputMagnitude = transform.localPosition.magnitude/frameDistance;

	    foreach (SpriteRenderer s in _spriterenderers)
	    {
	        Color c = s.color;
	        c.a = inputMagnitude+ idleAlpha;
	        s.color = c;
	    }

	    //foreach (SpriteRenderer s in camSpriteRenderers)
	    //{
	    //    Color c = s.color;
	    //    c.a = inputMagnitude>.3f?1:0;
	    //    s.color = c;
	    //}

	    if (Input.GetButtonDown("Fire1") && !_snapping)
	    {
	        _audioSource.pitch = Random.Range(.9f, 1.1f);
	        _audioSource.Play();
            StartCoroutine(SnapCr());
	        if (OnSnap != null)
    	        OnSnap();
	    }
    }

    IEnumerator SnapCr()
    {
        _snapping = true;
        yield return new WaitForSeconds(snapTime);
        _snapping = false;

    }
}
