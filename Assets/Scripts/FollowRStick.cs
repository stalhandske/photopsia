using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRStick : MonoBehaviour
{
    public float moveAmount;

    Vector2 _initLocalPos;

    void Awake()
    {
        _initLocalPos = transform.localPosition;
    }

    void Update()
    {
	    Vector2 moveInput = new Vector2(Input.GetAxis("HorizontalR"), Input.GetAxis("VerticalR"));
        transform.localPosition = _initLocalPos + moveInput*moveAmount;
    }
}
