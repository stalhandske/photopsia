using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedLineRenderer : MonoBehaviour {

    public int pointsCount=10;
    public float randomAmount;

    public LineRenderer lineRenderer;
    public Transform connection;

    Vector3[] points;

    private void Awake()
    {
        points = new Vector3[pointsCount];
    }

    private void Update()
    {
        lineRenderer.positionCount = pointsCount;

        Vector2 startPoint = transform.position;
        Vector2 endPoint = connection.position;

        for (int i = 0; i < pointsCount; i++)
        {
            points[i] = Vector2.Lerp(startPoint, endPoint, i / (pointsCount-1f))+((i!=0||i!=pointsCount-1)?Random.insideUnitCircle* randomAmount:Vector2.zero);
        }

        lineRenderer.SetPositions(points);
    }
}
