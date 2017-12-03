using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Arm : MonoBehaviour {

    public int smoothness = 5;
    public string sortingLayerName;
    public int sortingOrder;

    LineRenderer _lineRenderer;
    IK _ik;
    Vector3[] points;

	// Use this for initialization
	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.sortingOrder = sortingOrder;
	    _lineRenderer.sortingLayerName = sortingLayerName;
        _ik = GetComponent<IK>();

        points = new Vector3[3];

        _ik.DidUpdate += MyUpdate;
    }
	
	// Update is called once per frame
	void MyUpdate ()
    {
        points[0] = transform.position;
        points[1] = _ik.elbow.position;
        points[2] = _ik.hand.position;

        Vector3[] curvedPoints = Curver.MakeSmoothCurve(points, smoothness);

        _lineRenderer.SetPositions(curvedPoints);
        _lineRenderer.positionCount = curvedPoints.Length;
    }
}
