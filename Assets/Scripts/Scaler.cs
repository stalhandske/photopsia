
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public float closedTime;
    public float openTime;
    public AnimationCurve goingOpenCurve;
    public AnimationCurve goingClosedCurve;
    public bool changeOnJump;
    public Collider2D colliderToDisable;
    
    float _stateTime;
    State _state;
    float _startScale;

    void Awake()
    {
        _startScale = transform.localScale.x;
    }

    void ResetMe()
    {
        SetState(State.closed);
    }

    void HandleJump()
    {
        if (changeOnJump)
        {
            _stateTime = 999999;
        }
    }

    void HandleDisturbances(bool isDisturbed)
    {
        if (isDisturbed)
            SetState(State.disturbedClosed);
        else
            SetState(State.closed);
    }

    void HandleAttacking()
    {
        SetState(State.disturbedOpen);
    }

    void Update()
    {
        _stateTime += Time.deltaTime;

        switch (_state)
        {
            case State.closed:
                if (_stateTime >= closedTime)
                    SetState(State.goingOpen);
                break;
            case State.goingOpen:
                    transform.localScale = _startScale * Vector3.one * goingOpenCurve.Evaluate(_stateTime);

                    if (_stateTime >= goingOpenCurve.keys[goingOpenCurve.length-1].time)
                        SetState(State.open);
                break;
            case State.open:
                if (_stateTime >= openTime)
                    SetState(State.goingClosed);
                break;
            case State.goingClosed:
                transform.localScale = _startScale * Vector3.one * goingClosedCurve.Evaluate(_stateTime);

                if (_stateTime >= goingClosedCurve.keys[goingClosedCurve.length - 1].time)
                    SetState(State.closed);
                break;
        }
        
    }

    void SetState(State state)
    {
        _state = state;
        _stateTime = 0;

        switch (_state)
        {
            case State.closed:
                SetCollider(false);
                transform.localScale = _startScale * Vector3.one * goingOpenCurve.Evaluate(0);
                break;
            case State.goingOpen:
                SetCollider(true);
                break;
            case State.open:
                SetCollider(true);
                transform.localScale = _startScale * Vector3.one * goingOpenCurve.Evaluate(1);
                break;
            case State.goingClosed:
                SetCollider(false);
                break;
            case State.disturbedOpen: // going aggro
                SetCollider(true);
                transform.localScale = _startScale * Vector3.one * goingOpenCurve.Evaluate(1);
                break;
            case State.disturbedClosed:
                SetCollider(false);
                transform.localScale = _startScale * Vector3.one * goingOpenCurve.Evaluate(0);
                break;
        }
    }

    void SetCollider(bool setEnable)
    {
        if (colliderToDisable)
        {
            colliderToDisable.enabled = setEnable;
        }
    }

    public enum State
    {
        closed,
        goingOpen,
        open,
        goingClosed,
        disturbedClosed,
        disturbedOpen
    }
}
