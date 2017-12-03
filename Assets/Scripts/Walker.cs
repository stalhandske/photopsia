using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public float walkSpeed;
    public float walkFrequency;
    public AnimationCurve walkCurve;
    public float squashAmount;

    public NonPlayerCharacter nonPlayerCharacter;

    Vector2 direction;
    float walkCyclePosition;

	void Start ()
	{
	    direction = Random.insideUnitCircle.normalized;

	}
	
	void Update ()
	{
	    walkCyclePosition += Time.deltaTime* walkFrequency / Mathf.Pow(nonPlayerCharacter.size,2);
	    float moveAmount = walkSpeed * walkCurve.Evaluate(walkCyclePosition) * Time.deltaTime * Mathf.Pow(nonPlayerCharacter.size,.5f);

        transform.Translate(direction*moveAmount);
	    Vector3 localScale = transform.localScale;
	    localScale.x = nonPlayerCharacter.size * (1 - moveAmount * squashAmount);
        localScale.y = nonPlayerCharacter.size * (1 + moveAmount * squashAmount);
	    transform.localScale = localScale;
	}
}
