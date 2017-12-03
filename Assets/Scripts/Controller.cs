using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float movementSpeed;
    public float snapStunTime;

    public  ParticleSystem walkParticles;

    Rigidbody2D _rigidbody;
    bool _isSnapping;
    ParticleSystem.EmissionModule _emissionModule;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        FrameController.OnSnap += FrameController_OnSnap;
        _emissionModule = walkParticles.emission;
    }
    
    void Update ()
	{
	    Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	    moveInput = Vector2.ClampMagnitude(moveInput, 1);

	    _emissionModule.enabled = moveInput.magnitude > .3f;

	    RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + moveInput * .1f, moveInput, 300, LayerMask.GetMask("Wall"));
	    float actualSpeed = hit ? movementSpeed - movementSpeed / (1 + hit.distance) : movementSpeed;

        if (!_isSnapping)
            _rigidbody.velocity = actualSpeed * moveInput* moveInput.magnitude;
	}

    void FrameController_OnSnap()
    {
        StartCoroutine(SnapCr());
        _rigidbody.velocity = Vector2.zero;
    }

    IEnumerator SnapCr()
    {
        _isSnapping = true;
        yield return new WaitForSeconds(snapStunTime);
        _isSnapping = false;
    }
}
