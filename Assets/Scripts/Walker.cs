using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public float walkSpeed;
    public float walkFrequency;
    public AnimationCurve walkCurve;
    public float squashAmount;
    public float idleFrequency;
    public float lookDistance;
    public float distBeforeLooking;
    public float antiwallAffectant;

    public float minWalkTime;
    public float randWalkTime;

    public float hangAroundTime;

    public NonPlayerCharacter nonPlayerCharacter;

    Vector2 direction;
    float walkCyclePosition;
    float _stateTime;
    bool _isWalking;
    float walkTime;
    bool _isEntering = true;
    float _totalTimer;
    Collider2D _collider;
    bool _didBecomeVisible;
    TakePixOfMe _takePixOfMe;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _takePixOfMe = GetComponent<TakePixOfMe>();
        _takePixOfMe.onSnap += HandlePix;
    }

	void Start ()
	{
	    direction = -transform.position.normalized;
	}

    void Update()
    {
        _stateTime += Time.deltaTime;
        _totalTimer += Time.deltaTime;

        if (_isWalking)
        {
            walkCyclePosition += Time.deltaTime * walkFrequency / Mathf.Pow(nonPlayerCharacter.size, 2);
            float moveAmount = walkSpeed * walkCurve.Evaluate(walkCyclePosition) * Time.deltaTime *
                               Mathf.Pow(nonPlayerCharacter.size, -1f);

            transform.Translate(direction * moveAmount);

            Vector3 localScale = transform.localScale;
            localScale.x = nonPlayerCharacter.size * (1 - moveAmount * squashAmount);
            localScale.y = nonPlayerCharacter.size * (1 + moveAmount * squashAmount);
            transform.localScale = localScale;

            _collider.enabled = false;
            RaycastHit2D hit =  Physics2D.Raycast(transform.position + (Vector3)direction* distBeforeLooking, direction, lookDistance, LayerMask.GetMask("Antiwall") | LayerMask.GetMask("Tourist"));
            _collider.enabled = true;
            if (hit)
            {
                
                direction = (hit.distance / lookDistance) * direction + (1 - hit.distance / lookDistance) * hit.normal;
                direction = direction.normalized;
            }
        }
        else
        {
            walkCyclePosition = 0;
        }

        if (_stateTime > walkTime && walkCyclePosition%walkCurve.length < 0.1f)
        {
            _stateTime = 0;
            _isWalking = !_isWalking;
            walkTime = minWalkTime + randWalkTime * Random.value;

            if (_totalTimer > hangAroundTime)
                _isEntering = false;

            if (_isEntering)
                direction = ((Vector3)Random.insideUnitCircle - transform.position.normalized).normalized;
            else
                direction = ((Vector3)Random.insideUnitCircle + transform.position.normalized).normalized;
        }
    }

    void OnBecameVisible()
    {
        _didBecomeVisible = true;
    }

    void OnBecameInvisible()
    {
        if (_didBecomeVisible)
        {
            Destroy(gameObject);
        }
    }

    void HandlePix()
    {
        _isEntering = false;
        walkSpeed *= 3;
        _stateTime = 99;
        walkCyclePosition = 0;
        _isWalking = false;
    }
}
