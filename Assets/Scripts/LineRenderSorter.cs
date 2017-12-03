using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderSorter : MonoBehaviour
{

    public string sortingLayerName;
    public int sortingOrder;

    LineRenderer _lineRenderer;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.sortingOrder = sortingOrder;
        _lineRenderer.sortingLayerName = sortingLayerName;
    }
}
