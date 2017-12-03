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
    public GameObject dot;
    public AudioSource audioSourceClick;
    public AudioSource audioSourceBeep;

    Vector2 _target;
    SpriteRenderer[] _spriterenderers;
    bool _snapping;
    Collider2D _targetCol;
    TakePixOfMe _targetTakePixOfMe;

    void Awake()
    {
        _spriterenderers = GetComponentsInChildren<SpriteRenderer>();
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

	    _targetCol = Physics2D.OverlapCircle(transform.position,.5f, LayerMask.GetMask("Tourist"));

	    if (_targetCol && inputMagnitude > .2f)
	    {
	        dot.transform.localScale = Vector3.one * 3;

	        TakePixOfMe tpom = _targetCol.GetComponent<TakePixOfMe>();

	        if (tpom)
	        {
	            if (!_targetTakePixOfMe)
	            {
	                _targetTakePixOfMe = tpom;
                    _targetTakePixOfMe.SetInView();
	                audioSourceBeep.Play();
                }
	        }
	    }
	    else
	    {
	        dot.transform.localScale = Vector3.one;
	        if (_targetTakePixOfMe)
	        {
	            _targetTakePixOfMe.SetOutOfView();
	            _targetTakePixOfMe = null;
	        }
	    }

	    //foreach (SpriteRenderer s in camSpriteRenderers)
	    //{
	    //    Color c = s.color;
	    //    c.a = inputMagnitude>.3f?1:0;
	    //    s.color = c;
	    //}

	    if (Input.GetButtonDown("Fire1") && !_snapping)
	    {
	        audioSourceClick.pitch = Random.Range(.9f, 1.1f);
	        audioSourceClick.Play();
            StartCoroutine(SnapCr());
            if (_targetTakePixOfMe)
                _targetTakePixOfMe.TakeSnapShot();
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
